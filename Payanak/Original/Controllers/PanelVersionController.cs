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
    public class PanelVersionController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public PanelVersionController(SmsPanelDbContext context,
        IOptions<AppAuth> appAuth,
        IOptions<AppPath> appPath,
        IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _appAuth = appAuth.Value;
            _appPath = appPath.Value;
            _hostingEnvironment = hostingEnvironment;
        }
        public class serialModel
        {
            public string Serial { get; set; }
        }

        [HttpGet]
        public IActionResult Get([FromBody]serialModel model)
        {
            try
            {
                if (model == null ||
                    string.IsNullOrEmpty(model.Serial))
                    return BadRequest();

                var datas = model.Serial.Split("-");
                if (datas[0].ToUpper() != "HS.P" || datas.Length != 4)
                {
                    return StatusCode(ResponseStatus.wrongSerial);
                }

                var version = decimal.Parse(SmsService.GetPanelVersion(model.Serial));
                var res = _context.PanelVersionInfo.FirstOrDefault(a => a.MinVersion <= version
                                            && version <= a.MaxVersion);
                if (res == null)
                {
                    return StatusCode(ResponseStatus.panelVersionNotExist);
                }

                var panel = _context.VwPanel.FirstOrDefault(a => a.Serial.ToLower() == model.Serial.ToLower());
                if (panel == null)
                {
                     return StatusCode(ResponseStatus.panelNotExist);
                }

                dynamic dd = new
                {
                    Version = version,
                    Link = res.Path.Replace("~", Request.Scheme + "://" + Request.Host.ToString()),
                    Active = panel.IsBlocked.HasValue ? !panel.IsBlocked.Value : true,
                    Branch = panel.Name
                };

                return Ok(dd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
