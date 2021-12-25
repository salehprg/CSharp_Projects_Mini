using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class NumberInfo
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public bool? IsShared { get; set; }
        public bool? IsBlocked { get; set; }
        public long? Owner { get; set; }
        public short? Type { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? LastReceivedId { get; set; }

        public virtual AccountInfo OwnerNavigation { get; set; }
    }
}
