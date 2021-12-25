using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwTicketDetail
    {
        public long? Id { get; set; }
        public long? SenderId { get; set; }
        public long? TicketId { get; set; }
        public string Body { get; set; }
        public DateTime? SendDate { get; set; }
        public short? Status { get; set; }
        public string SenderUsername { get; set; }
        public string SenderEmail { get; set; }
        public string SenderMobilePhone { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public short? SenderGender { get; set; }
        public long? UserId { get; set; }
        public short? TicketStatus { get; set; }
        public string OwnerUsername { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerMobilePhone { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public short? OwnerGender { get; set; }
        public string OwnerPicture { get; set; }
        public string Header { get; set; }
        public string SenderPicture { get; set; }
    }
}
