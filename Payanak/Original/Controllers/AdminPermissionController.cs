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

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class AdminPermissionController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public AdminPermissionController(SmsPanelDbContext context,
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
        public IActionResult Get([FromQuery] string queryParam)
        {
            try
            {
                QueryParamModel qp = null;
                if (queryParam != null)
                    qp = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryParamModel>(queryParam);
                {
                    var permissions = _context.Permissions.Where(a => true);
                    var TotalCount = 0;
                    if (qp != null)
                    {
                        if (!string.IsNullOrEmpty(qp.Filter))
                        { // اعمال فیلتر سرچ شده

                            permissions = permissions.Where(
                                            a => a.Title.Contains(qp.Filter)
                                            );
                        }
                        if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                            permissions = permissions.OrderByStr(qp.SortField);

                        if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                            permissions = permissions.OrderByStrDescending(qp.SortField);

                        TotalCount = permissions.Count();
                        var skip = (qp.PageNumber - 1) * qp.PageSize;
                        if (TotalCount > qp.PageSize)
                            permissions = permissions.Skip(skip).Take(qp.PageSize);
                    }
                    var permissionsList = permissions.Select(a => a.ToModel()).ToList();

                    var res = Newtonsoft.Json.JsonConvert.SerializeObject(permissionsList);
                    return Ok(res);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                

                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user == null)
                    return Unauthorized();

                var group = _context.Group.FirstOrDefault(a => a.Id == id &&
                                                    a.Owner == user.Id); // TODO Get result for permissions
                if (group == null)
                {
                    return Unauthorized();
                }

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(group);
                return Ok(
                    res
                );
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody]GroupModel groupModel)
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user != null)
                {
                    if (groupModel == null ||
                    string.IsNullOrEmpty(groupModel.Name) ||
                    string.IsNullOrEmpty(groupModel.Title))

                        return BadRequest();
                    var res = new Group();
                    using (var _transaction = _context.Database.BeginTransaction())
                    {
                        res = new Group()
                        {
                            Name = groupModel.Name,
                            Title = groupModel.Title,
                            Descriptions = groupModel.Descriptions,
                            Status = 1,
                            Owner = user.Id,
                            Picture = string.IsNullOrEmpty(groupModel.Picture) ?
                            ("assets/img/portrait/avatars/avatar-08.png") :
                            (getImagePath(groupModel.Picture)),
                        };
                        _context.Group.Add(res);
                        _context.SaveChanges();
                        _transaction.Commit();
                    }
                    return Ok(res.ToModel());
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            try
            {
                

                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user == null)
                    return Unauthorized();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    var group = _context.Group.FirstOrDefault(a => a.Id == id);
                    if (group == null)
                    {
                        return BadRequest();
                    }

                    var userGroups = _context.UserGroups.Where(a => a.GroupId == id);
                    _context.UserGroups.RemoveRange(userGroups);
                    _context.Group.Remove(group);
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
        public string getImagePath(string src)
        {
            var time = DateTime.UtcNow;
            var guid = Guid.NewGuid();
            if (src.Substring(0, 20).ToLower().StartsWith("data:image")
                            || src.Length > 1000)// باید تصویر ذخیره شود
            {
                var img = ImageHelper.LoadImage(src);
                var tmpIndex = src.IndexOf(';');
                var extension = src.Substring(11, tmpIndex - 11).ToLower();
                var folder = "Images/" + String.Format("{0:MM_dd_yyyy}", time);
                if (!System.IO.Directory.Exists(_hostingEnvironment.ContentRootPath + "/" + folder))
                    System.IO.Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + "/" + folder);
                var path = folder + "/" + guid.ToString() + '.' + extension;
                ImageHelper.SaveImage(img, _hostingEnvironment.ContentRootPath + "/" + path, extension);
                return "~/" + path;
            }
            return src;
        }
        [Route("[action]/{id}")]
        [HttpPost("{id}")]
        [Authorize]
        public ActionResult CreateGroupForUser(long id, [FromBody]GroupModel groupModel)
        {
            try
            {
                // GroupModel groupModel = data.groupModel;
                // long id = data.id;
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user != null)
                {
                    if (groupModel == null ||
                    string.IsNullOrEmpty(groupModel.Name) ||
                    string.IsNullOrEmpty(groupModel.Title) ||
                    id == 0)

                        return BadRequest();
                    var res = new Group();
                    using (var _transaction = _context.Database.BeginTransaction())
                    {
                        res = new Group()
                        {
                            Name = groupModel.Name,
                            Title = groupModel.Title,
                            Descriptions = groupModel.Descriptions,
                            Status = 1,
                            Owner = id,
                            Picture = string.IsNullOrEmpty(groupModel.Picture) ?
                            ("assets/img/portrait/avatars/avatar-08.png") :
                            (getImagePath(groupModel.Picture)),
                        };
                        _context.Group.Add(res);
                        _context.SaveChanges();
                        _transaction.Commit();
                    }

                    return Ok(res.ToModel());
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
