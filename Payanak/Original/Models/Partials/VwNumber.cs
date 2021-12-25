using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwNumber
    {
        public NumberModel ToModel()
        {
            return new NumberModel()
            {
                Id = this.Id.Value,
                IsBlocked = this.IsBlocked.HasValue ? this.IsBlocked.Value : false,
                IsShared = this.IsShared.HasValue ? this.IsShared.Value : false,
                Owner = this.Owner.HasValue ? this.Owner.Value : -1,
                Password = "",
                Type = this.Type.HasValue ? this.Type.Value : (short)1,
                Username = this.Username,
                Number = this.Number,
                CreateDate = this.CreateDate.HasValue ? this.CreateDate.Value.Ticks : -1,
                User = this.Owner.HasValue ? new ContactModel{
                    Id = this.Owner.Value,
                    Username = this.OwnerUsername,
                    Address = this.Address,
                    FName = this.FirstName,
                    LName = this.LastName,
                    MobilePhone = this.MobilePhone,
                    FormId = this.FormGuid
                } : null,
            };
        }
    }
}
