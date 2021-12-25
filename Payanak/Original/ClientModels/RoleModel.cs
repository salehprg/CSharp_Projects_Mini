using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class RoleModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public List<long> Permissions { get; set; } = new List<long>();
    }
}