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

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class RegisterController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public RegisterController(SmsPanelDbContext context,
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
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user != null)
                {
                    return Ok(user.ToModel());
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]UserModel userModel)
        {
            try
            {
                if (userModel == null ||
                userModel.AccountInfo == null ||
                string.IsNullOrEmpty(userModel.AccountInfo.Username) ||
                string.IsNullOrEmpty(userModel.AccountInfo.MobilePhone))
                    return BadRequest();

                if (_context.AccountInfo.Any(a => a.Username == userModel.AccountInfo.Username))
                    return  StatusCode(ResponseStatus.usernameExist);

                AccountInfo accInf = null;
                AddressInfo addrsInfo = null;
                PersonalInfo persInfo = null;
                AdditionalInfo addInfo = null;
                var res = new UserModel();
                using (var _transaction = _context.Database.BeginTransaction())
                {
                    accInf = new AccountInfo()
                    {
                        Username = userModel.AccountInfo.Username,
                        BusinessPhone = PhoneNumberService.GetCorrectNumber(userModel.AccountInfo.BusinessPhone),
                        CreateDate = DateTime.UtcNow,
                        Email = EmailService.IsValidEmail(userModel.AccountInfo.Email) ? userModel.AccountInfo.Email : null,
                        HomePhone = PhoneNumberService.GetCorrectNumber(userModel.AccountInfo.HomePhone),
                        LastLogin = DateTime.UtcNow,
                        MobilePhone = PhoneNumberService.GetCorrectNumber(userModel.AccountInfo.MobilePhone),
                        Password = CryptoService.MD5Hash(
                           string.IsNullOrEmpty(userModel.AccountInfo.Password) ?
                            userModel.AccountInfo.MobilePhone :
                            userModel.AccountInfo.Password),
                        Picture = string.IsNullOrEmpty(userModel.AccountInfo.Picture) ?
                        ("assets/img/portrait/avatars/avatar-08.png") :
                        (getImagePath(userModel.AccountInfo.Picture)),
                    };
                    _context.AccountInfo.Add(accInf);
                    _context.SaveChanges();
                    res.AccountInfo = accInf.ToModel();
                    if (userModel.PersonalInfo != null)
                    {
                        DateTime? bd = null;
                        if (userModel.PersonalInfo.Birthday > 0)
                            bd = new DateTime(userModel.PersonalInfo.Birthday);
                        persInfo = new PersonalInfo
                        {
                            Birthday = bd,
                            BirthdayChangeCounter = 0,
                            FirstName = userModel.PersonalInfo.FName,
                            Gender = userModel.PersonalInfo.Gender,
                            IsBirthdayChanged = false,
                            LastName = userModel.PersonalInfo.LName,
                            NationalCode = userModel.PersonalInfo.NationalCode,
                            NickName = userModel.PersonalInfo.NickName,
                            UserId = accInf.Id,
                        };
                        _context.PersonalInfo.Add(persInfo);
                    }
                    if (userModel.AdditionalInfo != null)
                    {
                        DateTime? sd = null;
                        if (userModel.AdditionalInfo.SpecialDay > 0)
                            sd = new DateTime(userModel.AdditionalInfo.SpecialDay);
                        addInfo = new AdditionalInfo
                        {
                            InstagramLink = userModel.AdditionalInfo.Instagram,
                            TelegramLink = userModel.AdditionalInfo.Telegram,
                            IsSpecialDateChanged = false,
                            SpecialDate = sd,
                            SpecialDateCounter = 0,
                            UserId = accInf.Id
                        };
                        _context.AdditionalInfo.Add(addInfo);
                    }
                    if (userModel.AddressInfo != null &&
                        userModel.AddressInfo.Latitude != null &&
                        userModel.AddressInfo.Longitude != null)
                    {
                        double latitude = 0;
                        double longitude = 0;
                        if (!double.TryParse(userModel.AddressInfo.Latitude, out latitude) ||
                        !double.TryParse(userModel.AddressInfo.Longitude, out longitude))
                        {

                        }
                        addrsInfo = new AddressInfo
                        {
                            Address = userModel.AddressInfo.Address,
                            City = userModel.AddressInfo.City,
                            Latitude = latitude,
                            Longitude = longitude,
                            Region = userModel.AddressInfo.Region,
                            UserId = accInf.Id
                        };
                        _context.AddressInfo.Add(addrsInfo);

                    }
                    if(userModel.Roles != null) 
                    {
                        foreach (var item in userModel.Roles)
                        {
                            _context.UserRoles.Add(new UserRoles{
                                RoleId =  item,
                                UserId = accInf.Id
                            });
                        }
                    }
                    _context.SaveChanges();
                    if (persInfo != null)
                        res.PersonalInfo = persInfo.ToModel();
                    if (addInfo != null)
                        res.AdditionalInfo = addInfo.ToModel();
                    if (addrsInfo != null)
                        res.AddressInfo = addrsInfo.ToModel();
                    _transaction.Commit();
                }
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public string getImagePath(string src)
        {
            var time = DateTime.UtcNow;
            var guid = Guid.NewGuid();
            if (src.Substring(0, 20).ToLower().StartsWith("data:image")
                            || src.Length > 1000)// باید تصویر ذخیره شود
            {
                var img = ImageHelper.LoadImage(src);
                var tmpIndex = src.IndexOf(';');
                var extension = src.Substring(11, tmpIndex - 11).ToLower();
                var folder = "images\\" + String.Format("{0:MM_dd_yyyy}", time);
                if (!System.IO.Directory.Exists(_appPath.Assets + "\\" + folder))
                    System.IO.Directory.CreateDirectory(_appPath.Assets + "\\" + folder);
                var path = folder + "\\" + guid.ToString() + '.' + extension;
                ImageHelper.SaveImage(img, _appPath.Assets + "\\" + path, extension);
                return "~/" + path.Replace("\\","/");
            }
            return src;
        }
    }
}
