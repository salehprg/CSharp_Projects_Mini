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
    public class AdminUserCreditController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public AdminUserCreditController(SmsPanelDbContext context,
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
        public IActionResult Get(long id)
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user == null)
                    return Unauthorized();
                var contact = _context.VwContact.FirstOrDefault(a => a.Id == id); // TODO Get result for permissions
                if (contact == null)
                {
                    return BadRequest();
                }

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(contact.ToCreditModel());
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(long id, [FromBody]CreditModel model)
        {
            try
            {
                

                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user != null)
                {
                    if (model == null)

                        return BadRequest();
                    var credit = _context.CreditInfo.FirstOrDefault(a => a.UserId == id);
                    if (credit == null)
                    {
                        credit = new CreditInfo()
                        {
                            UserId = id,
                            Credit = model.Credit,
                            Discount = model.Discount
                        };
                        _context.CreditInfo.Add(credit);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var sum = credit.Credit.HasValue ? credit.Credit.Value : 0;
                        var dis = credit.Discount.HasValue ? credit.Discount.Value : 0;
                        credit.Credit = sum + model.Credit;
                        credit.Discount = model.Discount;
                        _context.CreditInfo.Update(credit);
                        _context.SaveChanges();
                    }

                    var contact = _context.VwContact.FirstOrDefault(a => a.Id == id);

                    return Ok(contact.ToCreditModel());
                }
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
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user == null)
                    return Unauthorized();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    var role = _context.Roles.FirstOrDefault(a => a.Id == id);
                    if (role == null || (role.CanDelete.HasValue && !role.CanDelete.Value))
                    {
                        return StatusCode(ResponseStatus.cantDelete
                        );
                    }
                    var rolePermissions = _context.RolePermissions.Where(a => a.RoleId == id);
                    _context.RolePermissions.RemoveRange(rolePermissions);
                    _context.Roles.Remove(role);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
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
                return "~/" + path;
            }
            return src;
        }
        [Route("[action]/{id}")]
        [HttpPost("{id}")]
        [Authorize]
        public ActionResult CreateGroupForUser(long id, [FromBody]GroupModel groupModel)
        {
            try
            {
                // GroupModel groupModel = data.groupModel;
                // long id = data.id;
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user != null)
                {
                    if (groupModel == null ||
                    string.IsNullOrEmpty(groupModel.Name) ||
                    string.IsNullOrEmpty(groupModel.Title) ||
                    id == 0)

                        return BadRequest();

                    var res = new Group();
                    using (var _transaction = _context.Database.BeginTransaction())
                    {
                        res = new Group()
                        {
                            Name = groupModel.Name,
                            Title = groupModel.Title,
                            Descriptions = groupModel.Descriptions,
                            Status = 1,
                            Owner = id,
                            Picture = string.IsNullOrEmpty(groupModel.Picture) ?
                            ("assets/img/portrait/avatars/avatar-08.png") :
                            (getImagePath(groupModel.Picture)),
                        };
                        _context.Group.Add(res);
                        _context.SaveChanges();
                        _transaction.Commit();
                    }

                    return Ok(res.ToModel());
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
