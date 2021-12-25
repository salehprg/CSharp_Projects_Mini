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
    public class DashboardController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public DashboardController(SmsPanelDbContext context,
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
        public IActionResult Get()
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.VwContact.FirstOrDefault(a => a.Username == username);
                // if (user == null)
                //     return Unauthorized();

                var userGroups = _context.Group.Where(a => a.Owner == user.Id)
                                                .Select(a => a.Id)
                                                .ToList();

                var contactsCount = _context.UserGroups.Where(a => userGroups.Contains(a.GroupId))
                                                .Count();

                var userPanels = _context.PanelInfo.Where(a => a.UserId == user.Id)
                                                .ToList();

                var credit = user.Credit.HasValue ? user.Credit.Value : 0;
                var today = DateTime.UtcNow.Date;
                var day_10 = today.AddDays(-9);
                var contact10Day = _context.UserGroups.Where(a => a.CreateDate > day_10
                                                && userGroups.Contains(a.GroupId)).ToList();
                var send10Day = _context.SentInfo.Where(a => a.SentDate > day_10
                                                && a.UserId == user.Id)
                                                .Select(a => new { a.Count, a.CalculatedCount, a.SentDate })
                                                .ToList();

                var addedContacts = new List<long>
                {
                    0,0,0,0,0,
                    0,0,0,0,0,
                };
                var countSms = new List<long>
                {
                    0,0,0,0,0,
                    0,0,0,0,0,
                };
                var calcCountSms = new List<long>
                {
                    0,0,0,0,0,
                    0,0,0,0,0,
                };
                var todayTimeSpan = new TimeSpan(today.Ticks);
                foreach (var item in send10Day)
                {
                    var itemTimeSpan = new TimeSpan(item.SentDate.Value.Ticks);
                    countSms[(todayTimeSpan.Days - itemTimeSpan.Days)] += item.Count.HasValue ? item.Count.Value : 0;
                    calcCountSms[(todayTimeSpan.Days - itemTimeSpan.Days)] += item.CalculatedCount.HasValue ? item.CalculatedCount.Value : 0;
                }
                foreach (var item in contact10Day)
                {
                    var itemTimeSpan = new TimeSpan(item.CreateDate.Ticks);
                    addedContacts[(todayTimeSpan.Days - itemTimeSpan.Days)]++;
                }

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                    Result = new DashboardModel
                    {
                        ContactCount = contactsCount,
                        Credit = credit,
                        GroupCount = userGroups.Count,
                        PanelCount = userPanels.Count,
                        Contacts10Day = addedContacts,
                        Count10Day = countSms,
                        CountCalc10Day= calcCountSms
                    },
                    Status = new List<ResponseStatusModel> {
                            new ResponseStatusModel (ResponseStatus.ok)
                        },
                    TotalCount = 1
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



    }
}
