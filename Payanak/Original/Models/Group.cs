using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Group
    {
        public Group()
        {
            UserGroups = new HashSet<UserGroups>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public long Owner { get; set; }
        public string Descriptions { get; set; }
        public short? Status { get; set; }
        public string Picture { get; set; }

        public virtual AccountInfo OwnerNavigation { get; set; }
        public virtual ICollection<UserGroups> UserGroups { get; set; }
    }
}
