using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class ReceiveSMSModel
    {
        public long Id {get;set;}
        public string Body { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string MsgId { get; set; }
        public long SentDate { get; set; }
        public long Count { get; set; }
    }
}