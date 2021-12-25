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
    public class UserPanelController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserPanelController(SmsPanelDbContext context,
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

                var panels = _context.VwPanel.Where(a => a.UserId == user.Id);
                if (!string.IsNullOrEmpty(qp.Filter))
                { // اعمال فیلتر سرچ شده
                    qp.Filter = qp.Filter.ToLower().Trim();
                    panels = panels.Where(
                                    a => a.Name.ToLower().Contains(qp.Filter) ||
                                        a.Serial.ToLower().Contains(qp.Filter)
                                    );
                }
                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                    panels = panels.OrderByStr(qp.SortField);

                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                    panels = panels.OrderByStrDescending(qp.SortField);

                var TotalCount = panels.Count();
                
                var skip = (qp.PageNumber - 1) * qp.PageSize;
                if (TotalCount > qp.PageSize)
                    panels = panels.Skip(skip).Take(qp.PageSize);

                var panelsList = panels.Select(a => a.ToModel()).ToList();

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(panelsList);
                return Ok(
                    res
                );
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize]
        public ActionResult Put([FromBody]PanelModel panelModel)
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

                var hasAdminPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Write));
                if (panelModel == null ||
                string.IsNullOrEmpty(panelModel.Name) ||
                string.IsNullOrEmpty(panelModel.Serial))
                    return BadRequest();

                var res = _context.PanelInfo.FirstOrDefault(a => a.Id == panelModel.Id &&
                                            (a.UserId == user.Id || hasAdminPermission));
                if (res == null)
                    return Unauthorized();

                res.Name = panelModel.Name;
                res.GroupId = panelModel.Group.Id;
                res.NumberId = (panelModel.SendNumber != null && panelModel.SendNumber.Id != -1)
                                    ? panelModel.SendNumber.Id : (long?)null;
                res.TemplateId = (panelModel.Template != null && panelModel.Template.Id != -1)
                                ? panelModel.Template.Id : (long?)null;
                _context.PanelInfo.Update(res);
                _context.SaveChanges();

                var result = _context.VwPanel.FirstOrDefault(a => a.Id == res.Id);

                return Ok(result.ToModel());
            }
            catch
            {
                return BadRequest();
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

                var hasAdminPermission = ((userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Delete)) ||
                                            (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Write)));

                var panel = _context.PanelInfo.FirstOrDefault(a => a.Id == id &&
                                                            (a.UserId == user.Id || hasAdminPermission));
                if (panel == null)
                {
                    return Unauthorized();
                }

                panel.Status = (short)(1 - (panel.Status.HasValue ? panel.Status.Value : 1));
                _context.PanelInfo.Update(panel);
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
