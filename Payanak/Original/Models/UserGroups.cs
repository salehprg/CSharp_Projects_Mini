using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class UserGroups
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Group Group { get; set; }
        public virtual AccountInfo User { get; set; }
    }
}
