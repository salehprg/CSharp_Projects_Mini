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
    public class UserGroupContactsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserGroupContactsController(SmsPanelDbContext context,
        IOptions<AppAuth> appAuth,
        IOptions<AppPath> appPath,
        IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _appAuth = appAuth.Value;
            _appPath = appPath.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id, [FromQuery] string queryParam)
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

                var hasAdminPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Read));

                var group = _context.Group.FirstOrDefault(a => a.Id == id &&
                                                    (a.Owner == user.Id || hasAdminPermission));
                if (group == null)
                {
                    return Unauthorized();
                }

                var contacts = new List<ContactModel>();
                var qp = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryParamModel>(queryParam);
                var contactGroups = _context.VwContactGroups.Where(a => a.GroupId == group.Id);
                if (!string.IsNullOrEmpty(qp.Filter))
                { // اعمال فیلتر سرچ شده
                    qp.Filter = qp.Filter.ToLower().Trim();
                    contactGroups = contactGroups.Where(
                                    a => a.Title.ToLower().Contains(qp.Filter) ||
                                        a.Descriptions.ToLower().Contains(qp.Filter) ||
                                        a.FirstName.ToLower().Contains(qp.Filter) ||
                                        a.LastName.ToLower().Contains(qp.Filter) ||
                                        a.MobilePhone.ToLower().Contains(qp.Filter) ||
                                        a.MobilePhone.Replace("-","").ToLower().Contains(qp.Filter) ||
                                        a.Name.ToLower().Contains(qp.Filter) ||
                                        a.NickName.ToLower().Contains(qp.Filter) ||
                                        a.Username.ToLower().Contains(qp.Filter)
                                    );
                }
                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                    contactGroups = contactGroups.OrderByStr(qp.SortField);

                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                    contactGroups = contactGroups.OrderByStrDescending(qp.SortField);

                var TotalCount = contactGroups.Count();

                var skip = (qp.PageNumber - 1) * qp.PageSize;
                if (TotalCount > qp.PageSize)
                    contactGroups = contactGroups.Skip(skip).Take(qp.PageSize);

                var contactGroupsList = contactGroups.Select(a => a.ToContactModel()).ToList();
                var res = Newtonsoft.Json.JsonConvert.SerializeObject(contactGroupsList);
                return Ok(
                    res
                );
            }
            catch
            {
                return BadRequest();
            }
        }

        
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(long id, [FromQuery] long groupId)
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
                var group = _context.Group.FirstOrDefault(a => a.Id == groupId &&
                                                        (a.Owner == user.Id || hasAdminPermission));
                if (group == null)
                {
                    return Unauthorized();
                }

                var userGroups = _context.UserGroups.Where(a => a.GroupId == group.Id &&
                                                                a.UserId == id);
                _context.UserGroups.RemoveRange(userGroups);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
