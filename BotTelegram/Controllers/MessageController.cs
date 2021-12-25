using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotTelegram.API;
using BotTelegram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BotTelegram.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages(string message , int chatId)
        {
            try
            {
                TelegramAPI telegramAPI = new TelegramAPI();

                UpdateModel result = await telegramAPI.GetUpdates();

                Console.WriteLine(result);

                return Ok(result.result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SendNewMessage(string message , int chatId)
        {
            try
            {
                TelegramAPI telegramAPI = new TelegramAPI();

                bool result = await telegramAPI.SendMessage(chatId , message);

                Console.WriteLine(result);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult GetNewMessage([FromBody]UpdateModel updateModel)
        {
            try
            {
                Message lastMessage = updateModel.result[updateModel.result.Count - 1].message;

                Console.WriteLine(lastMessage);

                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
