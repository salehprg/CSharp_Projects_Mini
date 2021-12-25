using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class DeviceInfo
    {
        public long UserId { get; set; }
        public string Os { get; set; }
        public string Platform { get; set; }
        public string Browser { get; set; }
        public DateTime? LastActivity { get; set; }
        public string IpAddress { get; set; }

        public virtual AccountInfo User { get; set; }
    }
}
