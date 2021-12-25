using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            RolePermissions = new HashSet<RolePermissions>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public short? Level { get; set; }
        public long? Parent { get; set; }

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
