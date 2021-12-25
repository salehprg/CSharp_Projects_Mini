using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Backend.Helpers.FarazSmsApi
{
    public class FarazSmsApi {

        public enum SocialType
        {
            Telegram,
            Viber
        }


        const string Username = "goldenstarc";
        const string Password = "hektug-fakbAm-0vypje";

        static string SendData(string Data , bool OutMessage = false)
        {
            WebRequest request = WebRequest.Create("http://ippanel.com/services.jspd");

            request.Method = "POST";

            string postData = Data;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);

            string[] Splited = responseFromServer.Split(',');

            string code = Splited[0].Remove(0 , 2);
            code = code.Remove(code.Length - 1 , 1);

            string Message = Splited[1].Remove(0 , 1);
            Message = Message.Remove(Message.Length - 2 , 2);

            reader.Close();
            dataStream.Close();
            response.Close();

            if(OutMessage)
            {
                return Message;
            }
            else
            {
                return code;
            }
		
        }
        public static string SendSms(string[] Numbers , string Message , string FromNumber)
        {
            string json = JsonConvert.SerializeObject(Numbers);

            string postData = "op=send&uname" + Username + "&pass=" + Password + "&message=" + Message +"&to="+json+"&from=+98" + FromNumber;

            return SendData(postData);
        }

        public static string SendSocial(string[] Numbers , string Message , string FromNumber , SocialType _social)
        {
            string json = JsonConvert.SerializeObject(Numbers);

            string SocialName = "";

            switch(_social)
            {
                case SocialType.Telegram:
                    SocialName = "telegram";
                    break;

                case SocialType.Viber :
                    SocialName = "Viber";
                    break;
            }

            string postData = "op=sendsocial&uname" + Username + "&pass=" + Password + "&message=" + Message +"&to="+json+"&from=+98" + FromNumber + "&type=" + SocialName;

            return SendData(postData);
        }
        
        public static string GetCredit(string[] Numbers , string Message , string FromNumber)
        {
            string json = JsonConvert.SerializeObject(Numbers);

            string postData = "op=credit&uname" + Username + "&pass=" + Password;

            return SendData(postData , true);
        }
        
    }
}