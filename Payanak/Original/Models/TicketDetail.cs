using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class TicketDetail
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public long TicketId { get; set; }
        public string Body { get; set; }
        public DateTime SendDate { get; set; }
        public short Status { get; set; }
    }
}
