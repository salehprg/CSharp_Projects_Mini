using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class AddressInfo
    {
        public long UserId { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual AccountInfo User { get; set; }
    }
}
