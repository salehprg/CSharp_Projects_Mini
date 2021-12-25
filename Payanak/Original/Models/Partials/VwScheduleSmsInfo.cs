using System;
using System.Collections.Generic;
using Backend.ClientModels;
namespace Backend.Models
{
    public partial class VwScheduleSmsInfo
    {
        public ScheduleSmsInfoModel ToModel()
        {
            return new ScheduleSmsInfoModel
            {
                Id = this.Id.Value,
                AddedDay = this.AddedDay.HasValue ? this.AddedDay.Value : 0,
                AddedMonth = this.AddedMonth.HasValue ? this.AddedMonth.Value : 0,
                AddedYear = this.AddedYear.HasValue ? this.AddedYear.Value : 0,
                Code = this.Code.HasValue ? this.Code.Value : -1,
                Name = this.Name,
                Status = this.Status.HasValue ? this.Status.Value : (short)1,
                Owner = new ContactModel
                {
                    Id = this.UserId.Value,
                    Username = this.OwnerUsername,
                    FName = this.OwnerFirstName,
                    Birthday = -1,
                    SpecialDay = -1,
                    Gender = this.OwnerGender.HasValue ? this.OwnerGender.Value : (short)0,
                    LName = this.OwnerLastName,
                    MobilePhone = this.OwnerMobilePhone,
                },
                Number = new NumberModel
                {
                    Id = NumberId.HasValue ? NumberId.Value : -1,
                    IsBlocked = SendIsBlocked.HasValue ? SendIsBlocked.Value : false,
                    IsShared = SendIsShared.HasValue ? SendIsShared.Value : false,
                    Number = SendNumber,
                    Username = SendUsername,
                    Password = SendPassword,
                    Type = SendType.HasValue ? SendType.Value : (short)1,
                },
                Template = new TemplateModel
                {
                    Body = this.TemplateBody,
                    Id = this.TemplateId.HasValue ? this.TemplateId.Value : -1,
                    Title = this.TemplateName,
                    UserId = this.UserId.Value
                }
            };
        }
    }
}
