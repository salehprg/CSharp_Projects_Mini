using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwPanel
    {
        public PanelModel ToModel()
        {
            return new PanelModel
            {
                CreateDate = this.CreateDate.HasValue ? this.CreateDate.Value.Ticks : -1,
                Group = new GroupModel
                {
                    Descriptions = this.GroupDescription,
                    Id = this.GroupId.HasValue ? this.GroupId.Value : -1,
                    Name = this.GroupName,
                    Title = this.GroupTitle,
                    Picture = this.GroupPicture,
                    Status = 1
                },
                HashId = this.HashId,
                Id = this.Id.Value,
                LastActivity = this.LastActivity.HasValue ? this.LastActivity.Value.Ticks : -1,
                Name = this.Name,
                Number = this.Number,
                Serial = this.Serial,
                User = new ContactModel
                {
                    FName = this.FirstName,
                    Birthday = this.Birthday.HasValue ? this.Birthday.Value.Ticks : -1,
                    SpecialDay = -1,
                    Gender = this.Gender.HasValue ? this.Gender.Value : (short)0,
                    Id = this.UserId.Value,
                    Latitude = this.Latitude.HasValue ? this.Latitude.Value.ToString() : "",
                    LName = this.LastName,
                    Longitude = this.Longitude.HasValue ? this.Longitude.Value.ToString() : "",
                    MobilePhone = this.MobilePhone,
                    FormId = this.FormGuid
                },
                Version = this.Version,
                IsBlocked = this.IsBlocked.HasValue ? this.IsBlocked.Value : false,
                Status = this.Status.HasValue ? this.Status.Value : (short)1,
                SendNumber = new NumberModel
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
                    Id = TemplateId.HasValue ? TemplateId.Value : -1,
                    Body = TemplateBody,
                    Title = TemplateName
                }
            };
        }
    }
}
