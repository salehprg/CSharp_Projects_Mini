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
    public class UserGroupsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserGroupsController(SmsPanelDbContext context,
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
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user != null)
                {
                    var groups = _context.UserGroups.Where(a => a.UserId == user.Id)
                                                    .Include(a => a.Group)
                                                    .Select(a => a.Group);
                    if (!string.IsNullOrEmpty(qp.Filter))
                    { // اعمال فیلتر سرچ شده

                        groups = groups.Where(
                                        a => a.Title.Contains(qp.Filter) ||
                                            a.Descriptions.Contains(qp.Filter)
                                        );
                    }
                    if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                        groups = groups.OrderByStr(qp.SortField);

                    if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                        groups = groups.OrderByStrDescending(qp.SortField);

                    var TotalCount = groups.Count();

                    var skip = (qp.PageNumber - 1) * qp.PageSize;

                    if (TotalCount > qp.PageSize)
                        groups = groups.Skip(skip).Take(qp.PageSize);

                    var groupsList = groups.Select(a => a.ToModel()).ToList();

                    var res = Newtonsoft.Json.JsonConvert.SerializeObject(groupsList);
                    return Ok(
                        res
                    );
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
                     return StatusCode(ResponseStatus.usernameExist);

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
                var folder = "images\\" + String.Format("{0:MM_dd_yyyy}", time);
                if (!System.IO.Directory.Exists(_appPath.Assets + "\\" + folder))
                    System.IO.Directory.CreateDirectory(_appPath.Assets + "\\" + folder);
                var path = folder + "\\" + guid.ToString() + '.' + extension;
                ImageHelper.SaveImage(img, _appPath.Assets + "\\" + path, extension);
                return path;
            }
            return src;
        }
    }
}
