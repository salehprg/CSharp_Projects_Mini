using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class RolePermissions
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual Roles Role { get; set; }
    }
}
