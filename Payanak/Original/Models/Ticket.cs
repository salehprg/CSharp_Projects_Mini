using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Ticket
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public long UserId { get; set; }
        public long? Responder { get; set; }
        public short Status { get; set; }
        public string Header { get; set; }
    }
}
