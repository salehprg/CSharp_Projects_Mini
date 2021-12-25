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
    public class UserScheduledSmsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserScheduledSmsController(SmsPanelDbContext context,
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
        [Authorize]
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

                var hasAdminPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Read));
                if (user != null)
                {
                    var ssi = _context.VwScheduleSmsInfo.Where(a => a.UserId == user.Id || hasAdminPermission);
                    if (!string.IsNullOrEmpty(qp.Filter))
                    { // اعمال فیلتر سرچ شده
                        qp.Filter = qp.Filter.ToLower().Trim();
                        ssi = ssi.Where(
                                        a => a.Name.ToLower().Contains(qp.Filter) ||
                                            a.Code.ToString().ToLower().Contains(qp.Filter)
                                        );
                    }
                    if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                        ssi = ssi.OrderByStr(qp.SortField);

                    if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                        ssi = ssi.OrderByStrDescending(qp.SortField);

                    var TotalCount = ssi.Count();

                    var skip = (qp.PageNumber - 1) * qp.PageSize;
                    if (TotalCount > qp.PageSize)
                        ssi = ssi.Skip(skip).Take(qp.PageSize);

                    var ssiList = ssi.Select(a => a.ToModel()).ToList();

                    var res = Newtonsoft.Json.JsonConvert.SerializeObject(ssiList);
                    return Ok(
                        res
                    );
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody]ScheduleSmsInfoModel model)
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.VwContact.FirstOrDefault(a => a.Username == username);
                if (user == null)
                {
                    return Unauthorized();
                }

                
                if (model == null || model.Template == null || model.Template.Id == -1 ||
                string.IsNullOrEmpty(model.Name) || model.Number == null || model.Number.Id == -1)
                    return BadRequest();
                    
                if (_context.VwScheduleSmsInfo.Any(a => a.Code == model.Code))
                    return StatusCode(ResponseStatus.ssiCodeExist);

                var ssi = new ScheduleSmsInfo
                {
                    AddedDay = model.AddedDay,
                    AddedMonth = model.AddedMonth,
                    AddedYear = model.AddedYear,
                    Code = model.Code,
                    Name = model.Name,
                    Status = 1,
                    UserId = user.Id,
                    CreateDate = DateTime.UtcNow,
                    TemplateId = model.Template.Id,
                    NumberId = model.Number.Id
                };

                _context.ScheduleSmsInfo.Add(ssi);
                _context.SaveChanges();
                var result = _context.VwScheduleSmsInfo.FirstOrDefault(a => a.Id == ssi.Id);

                return Ok(result.ToModel());
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Authorize]
        public ActionResult Put([FromBody]ScheduleSmsInfoModel model)
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

                var hasAdminPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Write));

                if (model == null || model.Template == null || model.Template.Id == -1 ||
                string.IsNullOrEmpty(model.Name) || model.Number == null || model.Number.Id == -1)
                    return BadRequest();
                    
                var res = _context.ScheduleSmsInfo.FirstOrDefault(a => a.Id == model.Id &&
                                                (a.UserId == user.Id || hasAdminPermission));
                if (res == null)
                    return Unauthorized();

                res.Name = model.Name;
                res.AddedYear = model.AddedYear;
                res.AddedMonth = model.AddedMonth;
                res.AddedDay = model.AddedDay;
                res.Code = model.Code;
                res.Status = model.Status;
                res.TemplateId = model.Template.Id;
                res.NumberId = model.Number.Id;
                _context.ScheduleSmsInfo.Update(res);
                _context.SaveChanges();
                var result = _context.VwScheduleSmsInfo.FirstOrDefault(a => a.Id == res.Id);

                return Ok(result.ToModel());
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

                var hasAdminPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Delete));
                var ssi = _context.ScheduleSmsInfo.FirstOrDefault(a => a.Id == id &&
                                                (a.UserId == user.Id || hasAdminPermission));
                if (ssi == null)
                {
                    return Unauthorized();
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var ssd = _context.ScheduleSmsDetail.Where(a => a.ParentId == id);
                    _context.ScheduleSmsDetail.RemoveRange(ssd);
                    _context.ScheduleSmsInfo.Remove(ssi);
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

    }
}
