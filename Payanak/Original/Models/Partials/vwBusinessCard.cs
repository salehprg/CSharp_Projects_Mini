using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwBusinessCard
    {
        public BusinessCardModel ToModel()
        {
            return new BusinessCardModel()
            {
                Id = this.Id.Value,
                CreateDate = this.CreateDate.Value.Ticks,
                Group = new GroupModel
                {
                    Id = this.GroupId.HasValue ? this.GroupId.Value : -1,
                    Name = this.GroupName,
                    Picture = this.GroupPicture,
                    Status = this.GroupStatus.HasValue ? this.GroupStatus.Value : (short)1,
                    Title = this.GroupTitle
                },
                IsBlocked = this.IsBlocked.HasValue ? this.IsBlocked.Value : false,
                Number = new NumberModel
                {
                    Id = this.NumberId.HasValue ? this.NumberId.Value : -1,
                    IsBlocked = this.NumberIsBlocked.HasValue ? this.NumberIsBlocked.Value : false,
                    IsShared = this.NumberIsShared.HasValue ? this.NumberIsShared.Value : false,
                    Owner = this.NumberOwner,
                    Number = this.NumberSend,
                    Username = this.NumberUsername,
                    Password = null,
                    Type = this.NumberType.Value,
                },
                Status = this.Status.HasValue ? this.Status.Value : (short)1,
                Template = new TemplateModel
                {
                    Id = this.TemplateId.HasValue ? this.TemplateId.Value : -1,
                    Body = this.TemplateBody,
                    Title = this.TemplateName
                },
                User = new ContactModel
                {
                    Id = this.UserId.Value,
                    FName = this.FirstName,
                    LName = this.LastName,
                    NickName = this.NickName,
                    Username = this.Username,
                    MobilePhone = this.MobilePhone,
                    Gender = this.Gender.HasValue ? this.Gender.Value : (short)1,
                    Birthday = this.Birthday.HasValue ? this.Birthday.Value.Ticks : -1,
                    FormId = this.FormGuid
                },
                Key = this.Key
            };
        }
    }
}
