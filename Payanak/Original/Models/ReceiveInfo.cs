using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class ReceiveInfo
    {
        public long Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime? Date { get; set; }
        public string MsgId { get; set; }
        public string Body { get; set; }
        public long? Count { get; set; }
    }
}
