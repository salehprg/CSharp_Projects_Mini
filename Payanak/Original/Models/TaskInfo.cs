using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class TaskInfo
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public int? Percent { get; set; }
        public string Message { get; set; }
        public short? Status { get; set; }
        public string Header { get; set; }
        public string Guid { get; set; }
    }
}
