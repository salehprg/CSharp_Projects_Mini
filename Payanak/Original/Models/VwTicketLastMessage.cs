using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwTicketLastMessage
    {
        public long? Id { get; set; }
        public long? SenderId { get; set; }
        public long? TicketId { get; set; }
        public string Body { get; set; }
        public DateTime? SendDate { get; set; }
        public short? MessageStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? UserId { get; set; }
        public long? Responder { get; set; }
        public short? Status { get; set; }
        public string Header { get; set; }
        public string OwnerUsername { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerMobilePhone { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public short? OwnerGender { get; set; }
        public string OwnerPicture { get; set; }
        public string ResponderUsername { get; set; }
        public string ResponderEmail { get; set; }
        public string ResponderMobilePhone { get; set; }
        public string ResponderFirstName { get; set; }
        public string ResponderLastName { get; set; }
        public short? ResponderGender { get; set; }
        public string ResponderPicture { get; set; }
        public long? Unread { get; set; }
    }
}
