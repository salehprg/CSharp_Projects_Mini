
using System.Collections.Generic;

namespace Backend.ClientModels{
    public class ChatModel
    {
        public string Avatar{get;set;}
        public string ChatClass{get;set;}
        public string ImagePath{get;set;}
        public string Time{get;set;}
        public List<string> Messages{get;set;}
        public string MessageType{get;set;}
        public long TicketId{get;set;}
    }
}