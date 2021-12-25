using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class SentInfo
    {
        public long Id { get; set; }
        public string Numbers { get; set; }
        public string GroupIds { get; set; }
        public int? Kind { get; set; }
        public short? Status { get; set; }
        public string Deliveries { get; set; }
        public string SendNumber { get; set; }
        public long? UserId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime? SentDate { get; set; }
        public string RectIds { get; set; }
        public long? Count { get; set; }
        public long? CalculatedCount { get; set; }
        public decimal? Price { get; set; }
    }
}
