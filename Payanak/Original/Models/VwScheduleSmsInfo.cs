using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwScheduleSmsInfo
    {
        public string Name { get; set; }
        public long? Code { get; set; }
        public int? AddedYear { get; set; }
        public int? AddedMonth { get; set; }
        public int? AddedDay { get; set; }
        public string OwnerUsername { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerMobilePhone { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public short? Discount { get; set; }
        public decimal? Credit { get; set; }
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public short? OwnerGender { get; set; }
        public short? Status { get; set; }
        public long? TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateBody { get; set; }
        public long? NumberId { get; set; }
        public string SendNumber { get; set; }
        public bool? SendIsShared { get; set; }
        public bool? SendIsBlocked { get; set; }
        public short? SendType { get; set; }
        public string SendUsername { get; set; }
        public string SendPassword { get; set; }
    }
}
