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
    public class UserBusinessCardController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserBusinessCardController(SmsPanelDbContext context,
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

                var businessCards = _context.VwBusinessCard.Where(a => a.UserId == user.Id); // TODO Permissions
                if (!string.IsNullOrEmpty(qp.Filter))
                { // اعمال فیلتر سرچ شده
                    qp.Filter = qp.Filter.ToLower().Trim();
                    businessCards = businessCards.Where(
                                    a => a.NumberSend.Contains(qp.Filter) ||
                                        a.GroupName.ToLower().Contains(qp.Filter) ||
                                        a.GroupTitle.ToLower().Contains(qp.Filter) ||
                                        a.TemplateName.ToLower().Contains(qp.Filter) ||
                                        a.Key.ToLower().Contains(qp.Filter)
                                    );
                }
                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                    businessCards = businessCards.OrderByStr(qp.SortField);

                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                    businessCards = businessCards.OrderByStrDescending(qp.SortField);

                var TotalCount = businessCards.Count();

                var skip = (qp.PageNumber - 1) * qp.PageSize;
                if (TotalCount > qp.PageSize)
                    businessCards = businessCards.Skip(skip).Take(qp.PageSize);

                var businessCardsList = businessCards.Select(a => a.ToModel()).ToList();

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                    Result = businessCardsList,
                    Status = new List<ResponseStatusModel> {
                            new ResponseStatusModel (ResponseStatus.ok)
                        },
                    TotalCount = businessCardsList.Count
                });

                return Ok(
                    res
                );
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

                var bc = _context.VwBusinessCard.FirstOrDefault(a => a.Id == id &&
                                                    (a.UserId == user.Id)); // TODO Get result for permissions
                if (bc == null)
                {
                    return Unauthorized();
                }

                 var res = Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                    Result = bc.ToModel(),
                    Status = new List<ResponseStatusModel> {
                            new ResponseStatusModel (ResponseStatus.ok)
                        },
                    TotalCount = 1
                });

                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Authorize]
        public ActionResult put([FromBody]BusinessCardModel model)
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

                var hasAdminPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Write));;
                
                if (model == null ||
                string.IsNullOrEmpty(model.Key) ||
                model.Number == null || model.Number.Id == -1 ||
                model.Template == null || model.Template.Id == -1 ||
                model.User == null || model.User.Id == -1)
                    return BadRequest();

                var res = _context.BusinessCard.FirstOrDefault(a => a.Id == model.Id &&
                                                (a.UserId == user.Id || hasAdminPermission));
                if (res == null)
                {
                    return Unauthorized();
                }

                if (res.Key != model.Key && _context.BusinessCard.Any(a => a.Key == model.Key))
                {
                     return StatusCode(ResponseStatus.keyExist);
                }

                res.Key = model.Key.Trim();
                res.NumberId = model.Number.Id;
                res.TemplateId = model.Template.Id;
                res.GroupId = model.Group.Id;
                _context.BusinessCard.Update(res);
                _context.SaveChanges();

                var bc = _context.VwBusinessCard.FirstOrDefault(a => a.Id == res.Id);

                return Ok(bc.ToModel());
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

                var hasAdminPermission = ((userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Delete)) ||
                                            (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.UserManagement) && userPermissions.Any(x => x.Name == AppPermissions.Write)));
                var bc = _context.BusinessCard.FirstOrDefault(a => a.Id == id &&
                                                            (a.UserId == user.Id || hasAdminPermission));
                if (bc == null)
                {
                    return Unauthorized();
                }

                bc.Status = bc.Status.HasValue ? (short)(1 - bc.Status.Value) : (short)0;
                _context.BusinessCard.Update(bc);
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
