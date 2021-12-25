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
    public class UserRolesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public UserRolesController(SmsPanelDbContext context,
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
                var user = _context.VwContact.FirstOrDefault(a => a.Username == username);
                if (user == null)
                    return Unauthorized();

                var ai = _context.AccountInfo.Where(a => a.Id == id)
                                            .Include(a => a.UserRoles)
                                                .ThenInclude(a => a.Role)
                                                .ThenInclude(a => a.RolePermissions)
                                            .FirstOrDefault(); // TODO Get result for permissions
                if (ai == null)
                {
                    return BadRequest();
                }

                var res = ai.UserRoles.Select(a => a.Role.ToModel());

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
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Read)]
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

                if (model == null)
                    return BadRequest();

                var res = new ContactModel();
                var ai = _context.AccountInfo.FirstOrDefault(a => a.Id == model.AccountInfo.Id);
                var roles = model.Roles != null ? model.Roles : new List<long>();
                if (ai == null)
                    return BadRequest();

                using (var _transaction = _context.Database.BeginTransaction())
                {
                    var userRoles = _context.UserRoles.Where(a=>a.UserId == model.AccountInfo.Id);
                    _context.UserRoles.RemoveRange(userRoles);
                    _context.SaveChanges();
                    foreach (var item in model.Roles)
                    {
                        _context.UserRoles.Add(new UserRoles{
                            UserId = model.AccountInfo.Id,
                            RoleId = item
                        });
                    }
                    _context.SaveChanges();
                    _transaction.Commit();
                }
                res = _context.VwContact.FirstOrDefault(a=>a.Id == model.AccountInfo.Id)?.ToModel();
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

        // [HttpDelete("{id}")]
        // [Authorize]
        // public IActionResult Delete(long id)
        // {
        //     try
        //     {
        //         var usernameClaim = User.Claims.FirstOrDefault(a => a.Type == "username");
        //         if (usernameClaim == null)
        //             return Unauthorized();
        //         var username = UserManager.GetUsername(User);
        //         var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
        //         if (user == null)
        //             return Unauthorized();
        //         using (var transaction = _context.Database.BeginTransaction())
        //         {
        //             var numberInfo = _context.NumberInfo.FirstOrDefault(a => a.Id == id); // TODO for permissions
        //             if (numberInfo == null)
        //             {
        //                 return Ok(new ResponseModel
        //                 {
        //                     Status = new List<ResponseStatusModel>{
        //                        new ResponseStatusModel(ResponseStatus.badRequest)
        //                    }
        //                 });
        //             }
        //             _context.NumberInfo.Remove(numberInfo);
        //             _context.SaveChanges();
        //             transaction.Commit();
        //         }
        //         return Ok(new ResponseModel
        //         {
        //             Status = new List<ResponseStatusModel>{
        //                        new ResponseStatusModel(ResponseStatus.ok)
        //                    }
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest();
        //     }

        // }
        // public string getImagePath(string src)
        // {
        //     var time = DateTime.UtcNow;
        //     var guid = Guid.NewGuid();
        //     if (src.Substring(0, 20).ToLower().StartsWith("data:image")
        //                     || src.Length > 1000)// باید تصویر ذخیره شود
        //     {
        //         var img = ImageHelper.LoadImage(src);
        //         var tmpIndex = src.IndexOf(';');
        //         var extension = src.Substring(11, tmpIndex - 11).ToLower();
        //         var folder = "Images/" + String.Format("{0:MM_dd_yyyy}", time);
        //         if (!System.IO.Directory.Exists(_hostingEnvironment.ContentRootPath + "/" + folder))
        //             System.IO.Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + "/" + folder);
        //         var path = folder + "/" + guid.ToString() + '.' + extension;
        //         ImageHelper.SaveImage(img, _hostingEnvironment.ContentRootPath + "/" + path, extension);
        //         return "~/" + path;
        //     }
        //     return src;
        // }
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
                return "~/" + path.Replace("\\", "/");
            }
            return src;
        }
    }
}
