using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class Permissions
    {
        public PermissionModel ToModel()
        {
            return new PermissionModel()
            {
                Level = this.Level.HasValue ? this.Level.Value : (short)1,
                Parent = this.Parent,
                Id = this.Id,
                Name = this.Name,
                Title = this.Title,
                Checked = false
            };
        }
    }
}
