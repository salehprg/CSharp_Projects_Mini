using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwScheduleSms
    {
        public ScheduleDetailModel ToModel()
        {
            return new ScheduleDetailModel
            {
                Counter = this.Counter.HasValue ? this.Counter.Value : 0,
                Date = this.Date.HasValue ? this.Date.Value.Ticks : -1,
                UpdatedDate = this.UpdatedDate.HasValue ? this.UpdatedDate.Value.Ticks : -1,
                Id = this.Id.Value,
                Parent = new ScheduleSmsInfoModel
                {
                    AddedDay = this.AddedDay.HasValue ? this.AddedDay.Value : 0,
                    AddedMonth = this.AddedMonth.HasValue ? this.AddedMonth.Value : 0,
                    AddedYear = this.AddedYear.HasValue ? this.AddedYear.Value : 0,
                    Code = this.ParentCode.Value,
                    Id = this.ParentId.Value,
                    Name = this.ParentName,
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
                    Owner = new ContactModel
                    {
                        Birthday = -1,
                        SpecialDay = -1,
                        Username = this.OwnerUsername,
                        Id = -1,
                        MobilePhone = this.OwnerMobilePhone,
                        LName = this.OwnerLastName,
                        FName = this.OwnerFirstName,
                        Gender = this.OwnerGender.Value
                    },
                    Status = 1,
                    Template = new TemplateModel
                    {
                        Id = this.TemplateId.HasValue ? this.TemplateId.Value : -1
                    }
                },
                User = new ContactModel
                {
                    Id = this.UserId.Value,
                    Birthday = this.Birthday.HasValue ? this.Birthday.Value.Ticks : -1,
                    SpecialDay = this.SpecialDate.HasValue ? this.SpecialDate.Value.Ticks : -1,
                    FName = this.FirstName,
                    LName = this.LastName,
                    MobilePhone = this.MobilePhone,
                    Username = this.Username,
                    Gender = this.Gender.HasValue ? this.Gender.Value : (short)1
                }
            };
        }
    }
}
