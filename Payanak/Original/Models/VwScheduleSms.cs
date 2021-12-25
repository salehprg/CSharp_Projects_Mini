using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwScheduleSms
    {
        public long? Id { get; set; }
        public long? ParentId { get; set; }
        public long? UserId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? Counter { get; set; }
        public string ParentName { get; set; }
        public long? ParentCode { get; set; }
        public int? AddedYear { get; set; }
        public int? AddedMonth { get; set; }
        public int? AddedDay { get; set; }
        public string OwnerUsername { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerMobilePhone { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public short? OwnerGender { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? SpecialDate { get; set; }
        public short? Gender { get; set; }
        public short? Discount { get; set; }
        public decimal? Credit { get; set; }
        public long? OwnerId { get; set; }
        public long? TemplateId { get; set; }
        public long? NumberId { get; set; }
        public string SendNumber { get; set; }
        public bool? SendIsShared { get; set; }
        public bool? SendIsBlocked { get; set; }
        public short? SendType { get; set; }
        public string SendUsername { get; set; }
        public string SendPassword { get; set; }
    }
}
