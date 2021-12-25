using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class ScheduleSmsDetail
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long? UserId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? Counter { get; set; }
    }
}
