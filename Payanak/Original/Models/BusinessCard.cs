using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class BusinessCard
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? GroupId { get; set; }
        public bool? IsBlocked { get; set; }
        public short? Status { get; set; }
        public long? TemplateId { get; set; }
        public long? NumberId { get; set; }
        public string Key { get; set; }
    }
}
