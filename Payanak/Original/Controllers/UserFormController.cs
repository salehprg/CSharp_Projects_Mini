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
    public class UserFormController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserFormController(SmsPanelDbContext context,
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
        public IActionResult Get([FromQuery] string fi)
        {
            try
            {
                var aii = _context.VwContact.FirstOrDefault(a => a.FormGuid == fi && a.IsFormValid.Value == 1);
                if (aii == null)
                {
                    return BadRequest();
                }

                var res = new UserModel();
                var ai = _context.AccountInfo.FirstOrDefault(a => a.Id == aii.Id);
                res.AccountInfo = ai.ToModel();
                var adrsInf = _context.AddressInfo.FirstOrDefault(a => a.UserId == ai.Id);
                if (adrsInf != null)
                {
                    res.AddressInfo = adrsInf.ToModel();
                }
                var pi = _context.PersonalInfo.FirstOrDefault(a => a.UserId == ai.Id);
                if (pi != null)
                {
                    res.PersonalInfo = pi.ToModel();
                }
                var addInf = _context.AdditionalInfo.FirstOrDefault(a => a.UserId == ai.Id);
                if (addInf != null)
                {
                    res.AdditionalInfo = addInf.ToModel();
                }
                res.Roles = ai.UserRoles.Select(a => a.RoleId).ToList();

                var result = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                return Ok(
                    result
                );
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
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
