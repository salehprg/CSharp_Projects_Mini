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
    public class AdminTicketController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private SmsPanelDbContext _context;
        private AppAuth _appAuth;
        private AppPath _appPath;
        public AdminTicketController(SmsPanelDbContext context,
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
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Read)]
        public IActionResult Get([FromQuery] string queryParam)
        {
            try
            {
                var qp = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryParamModel>(queryParam);
                

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


                var tickets = _context.VwTicket.Where(a => a.Status == 1);
                if (!string.IsNullOrEmpty(qp.Filter))
                { // اعمال فیلتر سرچ شده
                    qp.Filter = qp.Filter.ToLower().Trim();
                    tickets = tickets.Where(
                                    a => a.ResponderEmail.ToLower().Contains(qp.Filter) ||
                                        a.ResponderFirstName.ToLower().Contains(qp.Filter) ||
                                        a.ResponderLastName.ToLower().Contains(qp.Filter) ||
                                        a.ResponderUsername.ToLower().Contains(qp.Filter)
                                    );
                }
                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "asc") // اعمال ترتیب افزایشی
                    tickets = tickets.OrderByStr(qp.SortField);

                if (!string.IsNullOrEmpty(qp.SortField) && qp.SortOrder.ToLower() == "desc")// اعمال ترتیب کاهشی
                    tickets = tickets.OrderByStrDescending(qp.SortField);

                var TotalCount = tickets.Count();
                var skip = (qp.PageNumber - 1) * qp.PageSize;
                if (TotalCount > qp.PageSize)
                    tickets = tickets.Skip(skip).Take(qp.PageSize);

                var ticketsList = tickets.Select(a => a.ToModel()).ToList();
                var tikectIds = ticketsList.Select(a => a.Id);
                var lasts = _context.VwTicketLastMessage.Where(a => tikectIds.Contains(a.TicketId.Value))
                                                    .Select(a => a.ToModel(user))
                                                    .ToList();

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(lasts);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
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

                var td = _context.VwTicketDetail.Where(a => a.TicketId == id).OrderBy(a => a.SendDate).ToList(); // TODO Get result for permissions
                var tdd = _context.TicketDetail.Where(a => a.TicketId == id);
                tdd.ForEachAsync(a => a.Status = 2);
                _context.SaveChangesAsync();
                var result = new List<ChatModel>();
                foreach (var item in td)
                {
                    if (result.Count > 0 &&
                    ((item.SenderUsername == item.OwnerUsername && result.Last().Avatar == "right") ||
                        (item.SenderUsername != item.OwnerUsername && result.Last().Avatar == "left")))
                    {
                        result[result.Count - 1].Messages.Add(item.Body);
                        continue;
                    }
                    result.Add(new ChatModel
                    {
                        Avatar = item.SenderUsername == item.OwnerUsername ? "right" : "left",
                        ChatClass = item.SenderUsername == item.OwnerUsername ? "chat" : "chat chat-left",
                        ImagePath = item.SenderUsername == item.OwnerUsername ? item.SenderPicture : item.OwnerPicture,
                        Messages = new List<string>{
                            item.Body
                        },
                        MessageType = "text",
                        Time = "",
                        TicketId = item.TicketId.Value
                    });
                }

                var res = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] TicketModel model)
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
                if (user != null)
                {
                    if (model == null ||
                    string.IsNullOrEmpty(model.Header))

                        return BadRequest();
                    var ticket = new Ticket();
                    using (var _transaction = _context.Database.BeginTransaction())
                    {
                        ticket = new Ticket
                        {
                            CreateDate = DateTime.UtcNow,
                            Header = model.Header,
                            Status = 1,
                            UserId = user.Id
                        };
                        _context.Ticket.Add(ticket);
                        _context.SaveChanges();
                        _transaction.Commit();
                    }

                    var result = _context.VwTicket.FirstOrDefault(a => a.Id == ticket.Id);
                    return Ok(result.ToModel());
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Write)]
        public ActionResult Put([FromBody]ChatModel model)
        {
            try
            {
                
                var username = UserManager.GetUsername(User);
                var user = _context.AccountInfo.FirstOrDefault(a => a.Username == username);
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


                if (model == null ||
                model.TicketId == -1 ||
                model.Messages == null ||
                model.Messages.Count == 0 ||
                string.IsNullOrEmpty(model.Messages[0]))
                    return BadRequest();

                var td = new TicketDetail();
                using (var _transaction = _context.Database.BeginTransaction())
                {
                    td = new TicketDetail
                    {
                        Body = model.Messages[0],
                        SendDate = DateTime.UtcNow,
                        SenderId = user.Id,
                        TicketId = model.TicketId,
                        Status = 1
                    };
                    var tik = _context.Ticket.FirstOrDefault(a => a.Id == model.TicketId);
                    tik.Responder = user.Id;
                    _context.Ticket.Update(tik);
                    _context.TicketDetail.Add(td);
                    _context.SaveChanges();
                    _transaction.Commit();
                }
                var result = _context.VwTicketDetail.FirstOrDefault(a => a.Id == td.Id);

                return Ok(new ChatModel
                    {
                        Avatar = result.SenderUsername == result.OwnerUsername ? "right" : "left",
                        ChatClass = result.SenderUsername == result.OwnerUsername ? "chat" : "chat chat-left",
                        ImagePath = result.SenderUsername == result.OwnerUsername ? result.SenderPicture : result.OwnerPicture,
                        Messages = new List<string>{
                            result.Body
                        },
                        MessageType = "text",
                        Time = "",
                        TicketId = result.TicketId.Value
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles=AppRoles.UserManagement)]
        [Authorize(Policy=AppPermissions.Write + "," + AppPermissions.Delete)]
        public IActionResult Delete(long id)
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

                using (var transaction = _context.Database.BeginTransaction())
                {
                    var ticket = _context.Ticket.FirstOrDefault(a => a.Id == id);
                    if (ticket == null)
                    {
                        return BadRequest();
                    }

                    ticket.Status = 3;
                    _context.Ticket.Update(ticket);
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

    }
}
