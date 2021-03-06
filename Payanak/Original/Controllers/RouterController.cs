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

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class RouterController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _apppath;
        public RouterController(SmsPanelDbContext context,
        IOptions<AppAuth> appAuth,
        IOptions<AppPath> apppath,
        IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _appAuth = appAuth.Value;
            _apppath = apppath.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
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
                
                var hasNormalUserSendPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.NormalUser) && userPermissions.Any(x => x.Name == AppPermissions.Send));

                var hasPanelPermission = (userAcc.UserRoles.Any(x => x.Role.Name == AppRoles.PanelUser) && userPermissions.Any(x => x.Name == AppPermissions.Read));

                var res = new List<RouteInfo>();
                // Dashboard //////////////////////////
                var dashboard = new RouteInfo()
                {
                    path = "/dashboard/dashboard1",
                    icon = "ft-home",
                    title = "??????????????",
                    badge = "",
                    badgeClass = "",
                    Class = "",
                    isExternalLink = false
                };
                res.Add(dashboard);
                // cartable //////////////////////////
                if (hasAdminPermission || hasPanelPermission)
                {
                    var cartable = new RouteInfo()
                    {
                        path = "/cartable",
                        icon = "ft-clipboard",
                        title = "??????????????",
                        badge = "",
                        badgeClass = "",
                        Class = "has-sub",
                        isExternalLink = false
                    };
                    res.Add(cartable);
                    {
                        { // ????????????
                            var payanak = new RouteInfo()
                            {
                                path = "/cartable/payanak",
                                icon = "ft-message-circle",
                                title = "????????????",
                                badge = "",
                                badgeClass = "",
                                Class = "has-sub",
                                isExternalLink = false
                            };
                            cartable.submenu.Add(payanak);
                            {
                                var userGroups = new RouteInfo()
                                {
                                    path = "/cartable/payanak/addCredit",
                                    icon = "ft-credit-card",
                                    title = "???????????? ????????????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                payanak.submenu.Add(userGroups);
                                var userTicket = new RouteInfo()
                                {
                                    path = "/cartable/payanak/ticket",
                                    icon = "fa fa-sticky-note",
                                    title = "???????? ????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                payanak.submenu.Add(userTicket);
                            }
                        }
                        { // ????????
                            var personal = new RouteInfo()
                            {
                                path = "/cartable/personal",
                                icon = "ft-user",
                                title = "????????",
                                badge = "",
                                badgeClass = "",
                                Class = "has-sub",
                                isExternalLink = false
                            };
                            cartable.submenu.Add(personal);
                            {
                                var userOwnedGroups = new RouteInfo()
                                {
                                    path = "/cartable/personal/myGroups",
                                    icon = "ft-users",
                                    title = "???????? ?????? ????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                personal.submenu.Add(userOwnedGroups);
                                var usertemplates = new RouteInfo()
                                {
                                    path = "/cartable/personal/template",
                                    icon = "ft-file-text",
                                    title = "?????? ???????? ?????? ????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                personal.submenu.Add(usertemplates);
                                var userNumbers = new RouteInfo()
                                {
                                    path = "/cartable/personal/number",
                                    icon = "ft-phone",
                                    title = "?????????? ?????? ????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                personal.submenu.Add(userNumbers);
                                var userPanels = new RouteInfo()
                                {
                                    path = "/cartable/personal/panels",
                                    icon = "ft-cpu",
                                    title = "???????????? ?????? ????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                personal.submenu.Add(userPanels);
                                var userbc = new RouteInfo()
                                {
                                    path = "/cartable/personal/businessCard",
                                    icon = "fa fa-address-card",
                                    title = "???????? ?????????? ?????? ????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                personal.submenu.Add(userbc);
                                var userSS = new RouteInfo()
                                {
                                    path = "/cartable/personal/scheduledSms",
                                    icon = "ft-calendar",
                                    title = "?????????? ?????????????? ????",
                                    badge = "",
                                    badgeClass = "",
                                    Class = "",
                                    isExternalLink = false
                                };
                                personal.submenu.Add(userSS);
                                
                            }
                        }

                    }
                }
                // SMS //////////////////////////
                if (hasNormalUserSendPermission ||
                    hasAdminPermission ||
                    hasPanelPermission)
                {
                    var sms = new RouteInfo()
                    {
                        path = "/sms",
                        icon = "ft-message-square",
                        title = "???????????? ????????",
                        badge = "",
                        badgeClass = "",
                        Class = "has-sub",
                        isExternalLink = false
                    };
                    res.Add(sms);
                    {
                        var compose = new RouteInfo()
                        {
                            path = "/sms/compose",
                            icon = "fa fa-envelope",
                            title = "??????????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        sms.submenu.Add(compose);
                        var userEventSMS = new RouteInfo()
                        {
                            path = "/sms/eventlySMS",
                            icon = "ft-calendar",
                            title = "?????????? ??????????????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        sms.submenu.Add(userEventSMS);
                        var userPostalCode = new RouteInfo()
                        {
                            path = "/sms/MapPostalCode",
                            icon = "ft-calendar",
                            title = "?????????? ???? ???????? ???? ????????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        sms.submenu.Add(userPostalCode);
                        var sent = new RouteInfo()
                        {
                            path = "/sms/sent",
                            icon = "fa fa-paper-plane",
                            title = "???????? ?????? ????????????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        sms.submenu.Add(sent);
                        var received = new RouteInfo()
                        {
                            path = "/sms/received",
                            icon = "ft-inbox",
                            title = "???????? ?????? ??????????????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        sms.submenu.Add(received);
                    }
                }
                // number //////////////////////////
                if (hasAdminPermission)
                {
                    var number = new RouteInfo()
                    {
                        path = "/number",
                        icon = "fa fa-tty",
                        title = "???????????? ?????????? ????",
                        badge = "",
                        badgeClass = "",
                        Class = "has-sub",
                        isExternalLink = false
                    };
                    res.Add(number);
                    {
                        var assign = new RouteInfo()
                        {
                            path = "/number/assign",
                            icon = "ft-list",
                            title = "???????? ?????????? ????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        number.submenu.Add(assign);
                        // var numberList = new RouteInfo()
                        // {
                        //     path = "/number/list",
                        //     icon = "fa-comment-medical",
                        //     title = "???????? ?????????? ????",
                        //     badge = "",
                        //     badgeClass = "",
                        //     Class = "",
                        //     isExternalLink = false
                        // };
                        // number.submenu.Add(numberList);
                    }
                    // user management //////////////////////////
                    var userManagements = new RouteInfo()
                    {
                        path = "/userManagement",
                        icon = "ft-users",
                        title = "???????????? ??????????????",
                        badge = "",
                        badgeClass = "",
                        Class = "has-sub",
                        isExternalLink = false
                    };
                    res.Add(userManagements);
                    {
                        var listUser = new RouteInfo()
                        {
                            path = "/userManagement/listUser",
                            icon = "fa fa-users",
                            title = "???????? ??????????????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        userManagements.submenu.Add(listUser);
                        var ticketList = new RouteInfo()
                        {
                            path = "/userManagement/listTicket",
                            icon = "fa fa-sticky-note",
                            title = "???????? ???????? ????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        userManagements.submenu.Add(ticketList);
                        var listRole = new RouteInfo()
                        {
                            path = "/userManagement/listRole",
                            icon = "ft-command",
                            title = "?????? ????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        userManagements.submenu.Add(listRole);
                        // var addpanel = new RouteInfo()
                        // {
                        //     path = "/userManagement/addPanel",
                        //     icon = "ft-cpu",
                        //     title = "???????????? ??????",
                        //     badge = "",
                        //     badgeClass = "",
                        //     Class = "",
                        //     isExternalLink = false
                        // };
                        // userManagements.submenu.Add(addpanel);
                        var addBusinessCard = new RouteInfo()
                        {
                            path = "/userManagement/addBusinessCard",
                            icon = "fa fa-address-card",
                            title = "???????? ???????? ?????????? ????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        userManagements.submenu.Add(addBusinessCard);
                    }

                    // panel Management /////////////////////////
                    var panelManagements = new RouteInfo()
                    {
                        path = "/panelManagement",
                        icon = "ft-cpu",
                        title = "???????????? ????????????",
                        badge = "",
                        badgeClass = "",
                        Class = "has-sub",
                        isExternalLink = false
                    };
                    res.Add(panelManagements);
                    {
                        var listPanel = new RouteInfo()
                        {
                            path = "/panelManagement/listPanel",
                            icon = "ft-list",
                            title = "???????? ???????????? ????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        panelManagements.submenu.Add(listPanel);
                        var panelVersion = new RouteInfo()
                        {
                            path = "/panelManagement/panelVersion",
                            icon = "fa fa-qrcode",
                            title = "???????? ???????????? ????",
                            badge = "",
                            badgeClass = "",
                            Class = "",
                            isExternalLink = false
                        };
                        panelManagements.submenu.Add(panelVersion);
                    }
                }
                return Ok(new  {
                            Status = new List<ResponseStatusModel> {
                                new ResponseStatusModel (ResponseStatus.ok)
                            },
                            Result = res,
                            TotalCount = 1
                        });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
