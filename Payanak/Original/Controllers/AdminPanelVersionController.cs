using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Backend.ClientModels;
using Backend.Helpers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DeviceDetectorNET;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class AdminPanelVersionController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public AdminPanelVersionController(SmsPanelDbContext context,
        IOptions<AppAuth> appAuth,
        IOptions<AppPath> appPath,
        IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _appAuth = appAuth.Value;
            _appPath = appPath.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Read)]
        public IActionResult Get([FromQuery] string queryParam)
        {
            try
            {
                var qp = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryParamModel>(queryParam);
                

                var username = UserManager.GetUsername(User);

                var user = _context.VwContact.FirstOrDefault(a => a.Username == username);
                if (user == null)
                {
                    return Unauthorized();
                }

                var userAcc = _context.AccountInfo.Where(a => a.Username == username)
                                                .Include(a => a.UserRoles)
                                                    .ThenInclude(a => a.Role)
                                                        .ThenInclude(a => a.RolePermissions)
                                                            .ThenInclude(a => a.Permission)
                                                .FirstOrDefault();

                var userPermissions = userAcc.UserRoles
                                            .Select(a => a.Role.RolePermissions
                                                        .Select(b => b.Permission))
                                            .SelectMany(a => a)
                                            .ToList();


                var versions = _context.PanelVersionInfo.OrderByDescending(a => a.Id).Where(a => true);
                if (!string.IsNullOrEmpty(qp.Filter))
                { // اعمال فیلتر سرچ شده
                    qp.Filter = qp.Filter.ToLower().Trim();
                    versions = versions.Where(
                                    a => a.NickName.ToLower().Contains(qp.Filter) ||
                                        a.MinVersion.ToString().ToLower().Contains(qp.Filter) ||
                                        a.MaxVersion.ToString().ToLower().Contains(qp.Filter)
                                    );
                }

                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                    versions = versions.OrderByStr(qp.SortField);

                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                    versions = versions.OrderByStrDescending(qp.SortField);

                var TotalCount = versions.Count();

                var skip = (qp.PageNumber - 1) * qp.PageSize;
                if (TotalCount > qp.PageSize)
                    versions = versions.Skip(skip).Take(qp.PageSize);

                var versionsList = versions.Select(a => a.ToModel()).ToList();

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(versionsList);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Write)]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        public ActionResult Post()
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user == null)
                {
                    return Unauthorized();
                }

                var userAcc = _context.AccountInfo.Where(a => a.Username == username)
                                                .Include(a => a.UserRoles)
                                                    .ThenInclude(a => a.Role)
                                                        .ThenInclude(a => a.RolePermissions)
                                                            .ThenInclude(a => a.Permission)
                                                .FirstOrDefault();

                var userPermissions = userAcc.UserRoles
                                            .Select(a => a.Role.RolePermissions
                                                        .Select(b => b.Permission))
                                            .SelectMany(a => a)
                                            .ToList();

                var files = Request.Form.Files;
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<PanelVersionModel>(Request.Form["model"]);

                if (model == null ||
                files == null || files.Count == 0)
                    return BadRequest();

                var version = new PanelVersionInfo();
                using (var _transaction = _context.Database.BeginTransaction())
                {
                    var path = getFilePath(files[0]);
                    version = new PanelVersionInfo
                    {
                        CreateDate = DateTime.UtcNow,
                        MaxVersion = model.MaxVersion,
                        MinVersion = model.MinVersion,
                        NickName = model.Nickname,
                        Path = path,
                    };
                    _context.PanelVersionInfo.Add(version);
                    _context.SaveChanges();
                    _transaction.Commit();
                }
                return Ok(version.ToModel());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Delete)]
        public IActionResult Delete(long id)
        {
            try
            {
                var username = UserManager.GetUsername(User);
                var user = _context.VwContact.FirstOrDefault(a => a.Username == username);
                
                if (user == null)
                {
                    return Unauthorized();
                }

                var userAcc = _context.AccountInfo.Where(a => a.Username == username)
                                                .Include(a => a.UserRoles)
                                                    .ThenInclude(a => a.Role)
                                                        .ThenInclude(a => a.RolePermissions)
                                                            .ThenInclude(a => a.Permission)
                                                .FirstOrDefault();

                var userPermissions = userAcc.UserRoles
                                            .Select(a => a.Role.RolePermissions
                                                        .Select(b => b.Permission))
                                            .SelectMany(a => a)
                                            .ToList();
                
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var panel = _context.PanelVersionInfo.FirstOrDefault(a => a.Id == id); 
                    if (panel == null)
                    {
                        return BadRequest();
                    }

                    _context.PanelVersionInfo.Remove(panel);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        public string getFilePath(IFormFile src)
        {
            var time = DateTime.UtcNow;
            var guid = Guid.NewGuid();
            var extension = System.IO.Path.GetExtension(src.FileName).Replace(".", "");
            var folder = "Files/" + String.Format("{0:MM_dd_yyyy}", time);
            if (!System.IO.Directory.Exists(_hostingEnvironment.ContentRootPath + "/" + folder))
                System.IO.Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + "/" + folder);
            var path = folder + "/" + guid.ToString() + '.' + extension;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                src.CopyTo(fileStream);
            }
            return "~/" + path.Replace("\\", "/");
        }
    }
}
