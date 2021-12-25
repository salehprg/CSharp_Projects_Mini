using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Settings
    {
        public long Id { get; set; }
        public long? LastRecivedSmsId { get; set; }
        public decimal? SmsPrice { get; set; }
        public short? SmsDiscount { get; set; }
        public string FormKey { get; set; }
        public string FormMessage { get; set; }
    }
}
