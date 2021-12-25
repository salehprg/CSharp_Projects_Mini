using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class CreditInfo
    {
        public long UserId { get; set; }
        public short? Discount { get; set; }
        public decimal? Credit { get; set; }

        public virtual AccountInfo User { get; set; }
    }
}
