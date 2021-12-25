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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers 
{
    [ApiController]
    [Route ("api/[controller]")]
    [EnableCors ("AllowOrigin")]
    public class ChangePasswordController : ControllerBase {

        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        public ChangePasswordController (SmsPanelDbContext context,
            IOptions<AppAuth> appAuth) {
            _context = context;
            _appAuth = appAuth.Value;
        }

        public class ChngPasswordInput
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }
        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword ([FromBody]ChngPasswordInput inputModel) 
        {
            if (inputModel.NewPassword != null && inputModel.OldPassword != null)
            {
                
                    
                var username = UserManager.GetUsername(User);

                var UserAccInfo = _context.AccountInfo.FirstOrDefault(x => x.Username == username);

                if(CryptoService.MD5Hash(inputModel.OldPassword) == UserAccInfo.Password)
                {
                    UserAccInfo.Password = CryptoService.MD5Hash(inputModel.NewPassword);
                    _context.AccountInfo.Update(UserAccInfo);
                    _context.SaveChanges();

                    return Ok();
                }
                return BadRequest("پسورد قبلی اشتباه میباشد");
            }

            return BadRequest("اطلاعات را به صورت صحیح وارد نمایید");
        }
    }
}
