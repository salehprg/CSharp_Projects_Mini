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
    public class AddContactController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public AddContactController(SmsPanelDbContext context,
        IOptions<AppAuth> appAuth,
        IOptions<AppPath> appPath,
        IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _appAuth = appAuth.Value;
            _appPath = appPath.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Post([FromBody]AddContactModel model)
        {
            try
            {
                if (model == null ||
                    string.IsNullOrEmpty(model.Number) ||
                    string.IsNullOrEmpty(model.ProductId))

                    return BadRequest();

                var numbers = SmsService.fixNumbers(new List<string> { model.Number });
                if (numbers == null || numbers.Count != 1)
                {
                    return StatusCode(ResponseStatus.wrongNumber);
                }

                var datas = model.ProductId.Split("-");
                if (datas[0].ToUpper() != "HS.P" || datas.Length != 4)
                {
                    return StatusCode(ResponseStatus.wrongSerial);
                }

                var panel = _context.PanelInfo.FirstOrDefault(a => a.Serial == model.ProductId);
                var user = _context.AccountInfo.FirstOrDefault(a => a.MobilePhone == numbers[0]);

                if (panel == null)
                {
                    return StatusCode(ResponseStatus.wrongSerial);
                }
                if (user == null)
                {
                    using (var _transaction = _context.Database.BeginTransaction())
                    {
                        var newUser = new AccountInfo
                        {
                            CreateDate = DateTime.UtcNow,
                            LastLogin = DateTime.UtcNow,
                            MobilePhone = numbers[0],
                            Picture = "assets/img/portrait/avatars/avatar-08.png",
                            Username = numbers[0],
                            Password = numbers[0]
                        };

                        _context.AccountInfo.Add(newUser);
                        _context.SaveChanges();

                        _context.UserRoles.Add(new UserRoles
                        {
                            RoleId = 2, // کاربر عادی
                            UserId = newUser.Id
                        });

                        _context.SaveChanges();
                        _transaction.Commit();
                        user = newUser;
                    }
                }
                if (!panel.GroupId.HasValue)
                {
                    return StatusCode(ResponseStatus.noGroup);
                }

                var userGroup = _context.UserGroups.FirstOrDefault(a => a.UserId == user.Id &&
                                                                    a.GroupId == panel.GroupId.Value);
                if (userGroup == null)
                {
                    _context.UserGroups.Add(new UserGroups
                    {
                        GroupId = panel.GroupId.Value,
                        UserId = user.Id,
                        CreateDate = DateTime.UtcNow
                    });
                }

                _context.SaveChanges();

                if ((!panel.IsBlocked.HasValue || !panel.IsBlocked.Value)
                    && panel.NumberId.HasValue && panel.TemplateId.HasValue
                    && (!panel.Status.HasValue || panel.Status.Value == 1))
                {
                    var guid = TaskHelper.AddTask();
                    var sendNumber = _context.NumberInfo.FirstOrDefault(a => a.Id == panel.NumberId.Value
                                                                && ((a.IsShared.HasValue && a.IsShared.Value) || a.Owner == user.Id)
                                                                && (!a.IsBlocked.HasValue || !a.IsBlocked.Value)); // TODO for Permissions
                    var template = _context.PersonalTemplate.FirstOrDefault(a => a.Id == panel.TemplateId.Value);
                    var owner = _context.VwContact.FirstOrDefault(a => a.Id == panel.UserId);
                    if (sendNumber != null && template != null && owner != null)
                    {
                        Task.Factory.StartNew(async () =>
                        {
                            var rectIds = await SmsService.sendSmsToNumbers(
                                        sendNumber.Username,
                                        sendNumber.Password,
                                        sendNumber.Number,
                                        numbers,
                                        null,
                                        template.Body,
                                        "[WelcomeMessage]",
                                        guid,
                                        owner);
                            return;
                        });

                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
