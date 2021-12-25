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
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserController(SmsPanelDbContext context,
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
        public IActionResult Get(int id)
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user == null)
                    return Unauthorized();
                var ai = _context.AccountInfo.Where(a => a.Id == id)
                                            .Include(a => a.UserRoles)
                                            .FirstOrDefault(); // TODO Get result for permissions
                if (ai == null)
                {
                    return BadRequest();
                }

                var res = new UserModel();
                res.AccountInfo = ai.ToModel();
                var adrsInf = _context.AddressInfo.FirstOrDefault(a => a.UserId == id);
                if (adrsInf != null)
                {
                    res.AddressInfo = adrsInf.ToModel();
                }
                var pi = _context.PersonalInfo.FirstOrDefault(a => a.UserId == id);
                if (pi != null)
                {
                    res.PersonalInfo = pi.ToModel();
                }
                var addInf = _context.AdditionalInfo.FirstOrDefault(a => a.UserId == id);
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
        public ActionResult Put([FromBody]UserModel model)
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.VwContact.FirstOrDefault(a => a.Username == username);
                if (user == null)
                {
                    return Unauthorized();
                }

                if (model == null)
                    return BadRequest();

                var res = new UserModel();
                var ai = _context.AccountInfo.FirstOrDefault(a => a.Id == model.AccountInfo.Id);
                var adrs = _context.AddressInfo.FirstOrDefault(a => a.UserId == model.AccountInfo.Id);
                var addInf = _context.AdditionalInfo.FirstOrDefault(a => a.UserId == model.AccountInfo.Id);
                var prsInfo = _context.PersonalInfo.FirstOrDefault(a => a.UserId == model.AccountInfo.Id);

                if (ai == null)
                    return BadRequest();

                    if (prsInfo == null)
                    {
                        prsInfo = new PersonalInfo(){
                            UserId = ai.Id
                        };
                        _context.PersonalInfo.Add(prsInfo);
                    }
                    if(addInf == null)
                    {
                        addInf = new AdditionalInfo(){
                            UserId = ai.Id
                        };
                        _context.AdditionalInfo.Add(addInf);
                    }
                    if(adrs == null)
                    {
                        adrs = new AddressInfo(){
                            UserId = ai.Id
                        };
                        _context.AddressInfo.Add(adrs);
                    }
                    _context.SaveChanges();
                    
                using (var _transaction = _context.Database.BeginTransaction())
                {
                    ai.MobilePhone =PhoneNumberService.GetCorrectNumber(model.AccountInfo.MobilePhone) ;
                    if (!string.IsNullOrEmpty(model.AccountInfo.Password))
                        ai.Password = CryptoService.MD5Hash(model.AccountInfo.Password);
                    ai.Picture = string.IsNullOrEmpty(model.AccountInfo.Picture) ?
                        ("assets/img/portrait/avatars/avatar-08.png") :
                        (getImagePath(model.AccountInfo.Picture));
                    ai.BusinessPhone = PhoneNumberService.GetCorrectNumber(model.AccountInfo.BusinessPhone);
                    ai.Email = model.AccountInfo.Email;
                    ai.HomePhone = PhoneNumberService.GetCorrectNumber(model.AccountInfo.HomePhone);
                    _context.AccountInfo.Update(ai);
                    prsInfo.FirstName = model.PersonalInfo?.FName;
                    prsInfo.LastName = model.PersonalInfo?.LName;
                    var birthday = prsInfo.Birthday.HasValue ? prsInfo.Birthday.Value.Date.Ticks : 0;
                    if (model.PersonalInfo != null
                        && model.PersonalInfo.Birthday != -1
                        && model.PersonalInfo.Birthday != birthday
                        && (!prsInfo.BirthdayChangeCounter.HasValue || prsInfo.BirthdayChangeCounter.Value < 3))
                    {
                        prsInfo.Birthday = new DateTime(model.PersonalInfo.Birthday);
                        prsInfo.BirthdayChangeCounter = 
                            prsInfo.BirthdayChangeCounter.HasValue ? 
                                (short)(prsInfo.BirthdayChangeCounter.Value + 1) :
                                (short)1;
                        prsInfo.IsBirthdayChanged = true;
                    }
                    prsInfo.Gender = model.PersonalInfo.Gender;
                    prsInfo.NationalCode = model.PersonalInfo.NationalCode;
                    prsInfo.NickName = model.PersonalInfo.NickName;
                    _context.PersonalInfo.Update(prsInfo);
                    addInf.InstagramLink = model.AdditionalInfo?.Instagram;
                    addInf.TelegramLink = model.AdditionalInfo?.Telegram;
                    var specialDate = addInf.SpecialDate.HasValue ? addInf.SpecialDate.Value.Date.Ticks : 0;
                    if (model.AdditionalInfo != null
                        && model.AdditionalInfo.SpecialDay != -1
                        && model.AdditionalInfo.SpecialDay != specialDate
                        && (!addInf.SpecialDateCounter.HasValue || addInf.SpecialDateCounter.Value < 3))
                    {
                        addInf.SpecialDate = new DateTime(model.AdditionalInfo.SpecialDay);
                        addInf.SpecialDateCounter = 
                            addInf.SpecialDateCounter.HasValue ? 
                                (short)(addInf.SpecialDateCounter.Value + 1) :
                                (short)1;;
                        addInf.IsSpecialDateChanged = true;
                    }
                    _context.AdditionalInfo.Update(addInf);
                    adrs.Address = model.AddressInfo?.Address;
                    adrs.City = model.AddressInfo?.City;
                    adrs.Region = model.AddressInfo?.Region;
                     if (model.AddressInfo != null &&
                        model.AddressInfo.Latitude != null &&
                        model.AddressInfo.Longitude != null)
                    {
                        double latitude = 0;
                        double longitude = 0;
                        double.TryParse(model.AddressInfo.Latitude, out latitude);
                        double.TryParse(model.AddressInfo.Longitude, out longitude);
                        if(double.IsFinite(latitude))
                            adrs.Latitude = latitude;
                        if(double.IsFinite(longitude))
                            adrs.Longitude = longitude;
                    }
                    _context.AddressInfo.Update(adrs);
                    _context.SaveChanges();
                    _transaction.Commit();
                }

                res.AccountInfo = ai.ToModel();
                res.AddressInfo = adrs.ToModel();
                res.AdditionalInfo = addInf.ToModel();
                res.PersonalInfo = prsInfo.ToModel();
                res.Roles = new List<long>();

                return Ok(res);
            }
            catch (Exception ex)
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
                var folder = "Images/" + String.Format("{0:MM_dd_yyyy}", time);
                if (!System.IO.Directory.Exists(_hostingEnvironment.ContentRootPath + "/" + folder))
                    System.IO.Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + "/" + folder);
                var path = folder + "/" + guid.ToString() + '.' + extension;
                ImageHelper.SaveImage(img, _hostingEnvironment.ContentRootPath + "/" + path, extension);
                return "~/" + path.Replace("\\","/");
            }
            return src;
        }
    }
}
