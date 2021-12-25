using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.ClientModels;
using Backend.Helpers;
using Backend.Models;
using DeviceDetectorNET;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Backend.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    [EnableCors ("AllowOrigin")]
    public class AdminNumberController : ControllerBase {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public AdminNumberController (SmsPanelDbContext context,
            IOptions<AppAuth> appAuth,
            IOptions<AppPath> appPath,
            IWebHostEnvironment hostingEnvironment) {
            _context = context;
            _appAuth = appAuth.Value;
            _appPath = appPath.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Read)]
        public IActionResult Get ([FromQuery] string queryParam) {
            try {
                var qp = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryParamModel> (queryParam);
                
                var username = UserManager.GetUsername(User);
                var user = _context.VwContact.FirstOrDefault (a => a.Username == username);
                if (user == null) {
                    return Unauthorized ();
                }

                


                var numbers = _context.NumberInfo.Where (a => true);

                if (!string.IsNullOrEmpty (qp.Filter)) { // اعمال فیلتر سرچ شده
                    qp.Filter = qp.Filter.ToLower ().Trim ();
                    numbers = numbers.Where (
                        a => a.Number.ToLower ().Contains (qp.Filter)
                    );
                }

                if (!string.IsNullOrEmpty (qp.SortField) && qp.SortOrder.ToLower () == "asc") // اعمال ترتیب افزایشی
                    numbers = numbers.OrderByStr (qp.SortField);

                if (!string.IsNullOrEmpty (qp.SortField) && qp.SortOrder.ToLower () == "desc") // اعمال ترتیب کاهشی
                    numbers = numbers.OrderByStrDescending (qp.SortField);

                var TotalCount = numbers.Count ();

                var skip = (qp.PageNumber - 1) * qp.PageSize;

                if (TotalCount > qp.PageSize)
                    numbers = numbers.Skip (skip).Take (qp.PageSize);

                var numberIds = numbers.Select (a => a.Id).ToList ();
                var numbersList = _context.VwNumber.Where (a => numberIds.Contains (a.Id.Value))
                    .Select (a => a.ToModel ())
                    .ToList ();

                return Ok (Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                        Result = numbersList,
                        Status = new List<ResponseStatusModel> {
                                new ResponseStatusModel (ResponseStatus.ok)
                            },
                        TotalCount = 1
                    }));

            } catch {
                return BadRequest ();
            }
        }

        [Route ("[action]")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers () {
            try {
                

                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault (a => a.Username == username);
                if (user == null)
                    return Unauthorized ();


                var contactAcc = _context.AccountInfo.Where (a => a.UserRoles.Any (b => b.Role.RolePermissions.Any (c => c.PermissionId == 11))).Select (a => a.Id).ToList ();
                var contacts = _context.VwContact.Where (a => contactAcc.Contains (a.Id.Value)).Select (a => a.ToModel ()).ToList (); // TODO get for permissions                // TODO get for permissions

                return Ok (Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                        Result = contacts,
                        Status = new List<ResponseStatusModel> {
                                new ResponseStatusModel (ResponseStatus.ok)
                            },
                        TotalCount = 1
                    }));
            } catch {
                return BadRequest ();
            }
        }

        [HttpGet ("{id}")]
        [Authorize]
        public IActionResult Get (int id) {
            try {
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault (a => a.Username == username);
                if (user == null)
                    return Unauthorized ();

                var number = _context.VwNumber.FirstOrDefault (a => a.Id == id); // TODO Get result for permissions
                if (number == null) {
                    return Unauthorized();
                }

                return Ok (
                    Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                        Result = number,
                        Status = new List<ResponseStatusModel> {
                                new ResponseStatusModel (ResponseStatus.ok)
                            },
                        TotalCount = 1
                    })
                );
            } catch {
                return BadRequest ();
            }
        }

        [HttpPost]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Write)]
        public ActionResult Post ([FromBody] NumberModel numberModel) {
            try {
                
                var username = UserManager.GetUsername(User);
                var user = _context.VwContact.FirstOrDefault (a => a.Username == username);
                if (user == null) {
                    return Unauthorized ();
                }
                var userAcc = _context.AccountInfo.Where (a => a.Username == username)
                    .Include (a => a.UserRoles)
                    .ThenInclude (a => a.Role)
                    .ThenInclude (a => a.RolePermissions)
                    .ThenInclude (a => a.Permission)
                    .FirstOrDefault ();
                var userPermissions = userAcc.UserRoles
                    .Select (a => a.Role.RolePermissions
                        .Select (b => b.Permission))
                    .SelectMany (a => a)
                    .ToList ();
                
                if (numberModel == null ||
                    string.IsNullOrEmpty (numberModel.Number) ||
                    string.IsNullOrEmpty (numberModel.Username) ||
                    string.IsNullOrEmpty (numberModel.Password))
                    return Ok (new ResponseModel {
                        Status = new List<ResponseStatusModel> {
                            new ResponseStatusModel (ResponseStatus.badRequest)
                        }
                    });
                var res = new NumberInfo ();
                var isShared = !numberModel.Owner.HasValue || numberModel.Owner.Value == -1;
                using (var _transaction = _context.Database.BeginTransaction ()) {
                    res = new NumberInfo () {
                    IsBlocked = false,
                    CreateDate = DateTime.UtcNow,
                    IsShared = isShared || numberModel.IsShared,
                    Number = numberModel.Number,
                    Owner = numberModel.Owner == -1 ? user.Id : numberModel.Owner,
                    Password = numberModel.Password,
                    Type = (numberModel.Type >= 0 && numberModel.Type <= 1) ? numberModel.Type : (short) 1,
                    Username = numberModel.Username
                    };
                    _context.NumberInfo.Add (res);
                    _context.SaveChanges ();
                    _transaction.Commit ();
                }
                return Ok (Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                        Result = res.ToModel(),
                        Status = new List<ResponseStatusModel> {
                                new ResponseStatusModel (ResponseStatus.ok)
                            },
                        TotalCount = 1
                    }));
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Write)]
        public ActionResult Put ([FromBody] NumberModel numberModel) {
            try {
                
                var username = UserManager.GetUsername(User);
                var user = _context.VwContact.FirstOrDefault (a => a.Username == username);
                if (user == null) {
                    return Unauthorized ();
                }
                var userAcc = _context.AccountInfo.Where (a => a.Username == username)
                    .Include (a => a.UserRoles)
                    .ThenInclude (a => a.Role)
                    .ThenInclude (a => a.RolePermissions)
                    .ThenInclude (a => a.Permission)
                    .FirstOrDefault ();
                var userPermissions = userAcc.UserRoles
                    .Select (a => a.Role.RolePermissions
                        .Select (b => b.Permission))
                    .SelectMany (a => a)
                    .ToList ();

                if (numberModel == null)
                    return BadRequest();

                var res = _context.NumberInfo.FirstOrDefault (a => a.Id == numberModel.Id);
                if (res == null)
                    return BadRequest();

                var isShared = !numberModel.Owner.HasValue || numberModel.Owner.Value == -1;
                using (var _transaction = _context.Database.BeginTransaction ()) {
                    res.IsBlocked = numberModel.IsBlocked;
                    res.Password = string.IsNullOrEmpty (numberModel.Password) ? res.Password : numberModel.Password;
                    res.Username = numberModel.Username;
                    res.Owner = numberModel.Owner != -1 ? numberModel.Owner : null;
                    res.IsShared = isShared || numberModel.IsShared;
                    _context.NumberInfo.Update (res);
                    _context.SaveChanges ();
                    _transaction.Commit ();
                }
                return Ok (Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseModel{
                        Result = res.ToModel(),
                        Status = new List<ResponseStatusModel> {
                                new ResponseStatusModel (ResponseStatus.ok)
                            },
                        TotalCount = 1
                    }));

            } catch {
                return BadRequest ();
            }
        }

        [HttpDelete ("{id}")]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Delete)]
        public IActionResult Delete (long id) {
            try {

                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault (a => a.Username == username);
                if (user == null) {
                    return Unauthorized ();
                }


                

                using (var transaction = _context.Database.BeginTransaction ()) {
                    var numberInfo = _context.NumberInfo.FirstOrDefault (a => a.Id == id);
                    if (numberInfo == null) {
                        return BadRequest();
                    }

                    _context.NumberInfo.Remove (numberInfo);
                    _context.SaveChanges ();
                    transaction.Commit ();
                }
                return Ok ();
            } catch {
                return BadRequest ();
            }

        }
        public string getImagePath (string src) {
            var time = DateTime.UtcNow;
            var guid = Guid.NewGuid ();
            if (src.Substring (0, 20).ToLower ().StartsWith ("data:image") ||
                src.Length > 1000) // باید تصویر ذخیره شود
            {
                var img = ImageHelper.LoadImage (src);
                var tmpIndex = src.IndexOf (';');
                var extension = src.Substring (11, tmpIndex - 11).ToLower ();
                var folder = "Images/" + String.Format ("{0:MM_dd_yyyy}", time);
                if (!System.IO.Directory.Exists (_hostingEnvironment.ContentRootPath + "/" + folder))
                    System.IO.Directory.CreateDirectory (_hostingEnvironment.ContentRootPath + "/" + folder);
                var path = folder + "/" + guid.ToString () + '.' + extension;
                ImageHelper.SaveImage (img, _hostingEnvironment.ContentRootPath + "/" + path, extension);
                return "~/" + path;
            }
            return src;
        }
    }
}