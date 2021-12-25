using System;
using System.Collections.Generic;
using System.Collections;
using Backend.ClientModels;
using System.Linq;

namespace Backend.Models
{
    public partial class Roles
    {
        public RoleModel ToModel()
        {
            return new RoleModel()
            {
                CanDelete = this.CanDelete.HasValue ? this.CanDelete.Value : true,
                CanEdit = this.CanEdit.HasValue ? this.CanEdit.Value : true,
                Id = this.Id,
                Name = this.Name,
                Title = this.Title,
                Permissions = this.RolePermissions !=null ? 
                            this.RolePermissions.Select(a=>a.PermissionId).ToList() : 
                            new List<long>{}
            };
        }
    }
}
