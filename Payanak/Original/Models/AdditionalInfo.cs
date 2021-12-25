using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class AdditionalInfo
    {
        public long UserId { get; set; }
        public DateTime? SpecialDate { get; set; }
        public short? SpecialDateCounter { get; set; }
        public string InstagramLink { get; set; }
        public string TelegramLink { get; set; }
        public bool? IsSpecialDateChanged { get; set; }

        public virtual AccountInfo User { get; set; }
    }
}
