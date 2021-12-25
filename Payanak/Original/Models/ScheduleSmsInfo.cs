using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class ScheduleSmsInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Code { get; set; }
        public long? UserId { get; set; }
        public int? AddedYear { get; set; }
        public int? AddedMonth { get; set; }
        public int? AddedDay { get; set; }
        public short? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? TemplateId { get; set; }
        public long? NumberId { get; set; }
    }
}
