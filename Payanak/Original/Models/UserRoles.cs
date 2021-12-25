using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class UserRoles
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual AccountInfo User { get; set; }
    }
}
