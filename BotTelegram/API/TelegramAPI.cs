using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BotTelegram.Models;

namespace BotTelegram.API
{
    public class TelegramAPI
    {
        private string token = "YourToken";
        HttpClient client;
        public TelegramAPI ()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.telegram.org/bot" + token + "/");
        }
        public async Task<UpdateModel> GetUpdates()
        {
            string request = "getUpdates";
            HttpResponseMessage response = await client.GetAsync(request);
            string responseJSON = await response.Content.ReadAsStringAsync();

            UpdateModel model = JsonSerializer.Deserialize<UpdateModel>(responseJSON);
            
            return model;
        }
        public async Task<Chat> GetChatInfo(int chatid)
        {
            string request = "getChat?chat_id="+ chatid;
            HttpResponseMessage response = await client.GetAsync(request);
            string responseJSON = await response.Content.ReadAsStringAsync();

            Chat model = JsonSerializer.Deserialize<Chat>(responseJSON);
            
            return model;
        }

        public async Task<bool> SendMessage(int chatid , string message)
        {
            string request = "sendMessage?chat_id="+ chatid +"&text=" + message;
            HttpResponseMessage response = await client.GetAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMessage(int chatid , int messageid)
        {
            string request = "deleteMessage?chat_id="+ chatid +"&message_id=" + messageid;
            HttpResponseMessage response = await client.GetAsync(request);

            return response.IsSuccessStatusCode;
        }

        // public async Task<bool> GetChatInfo()
        // {
            
        // }
    }
}