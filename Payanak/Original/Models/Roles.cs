using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Roles
    {
        public Roles()
        {
            RolePermissions = new HashSet<RolePermissions>();
            UserRoles = new HashSet<UserRoles>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanDelete { get; set; }

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
