using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwTicketLastMessage
    {
        public TicketModel ToModel(VwContact me)
        {
            return new TicketModel
            {
                Id = this.TicketId.Value,
                CreateDate = this.CreateDate.HasValue ? this.CreateDate.Value.Ticks : -1,
                Responder = this.Responder.HasValue ?
                    new ContactModel
                    {
                        Id = this.Responder.Value,
                        FName = this.ResponderFirstName,
                        LName = this.ResponderLastName,
                        Gender = this.ResponderGender.HasValue ? this.ResponderGender.Value : (short)1,
                        MobilePhone = this.ResponderMobilePhone,
                        Picture = this.ResponderPicture,
                        Username = this.ResponderUsername
                    } :
                    null,
                Status = this.Status.HasValue ? this.Status.Value : (short)1,
                User = new ContactModel
                {
                    Id = this.UserId.Value,
                    FName = this.OwnerFirstName,
                    LName = this.OwnerLastName,
                    Gender = this.OwnerGender.HasValue ? this.OwnerGender.Value : (short)1,
                    MobilePhone = this.OwnerMobilePhone,
                    Picture = this.OwnerPicture,
                    Username = this.OwnerUsername
                },
                Header = this.Header,
                LastMessage = this.Body == null? "---" :( this.Body.Length > 15 ? this.Body.Substring(0, 12) + "..." : this.Body),
                Unread = !this.SenderId.HasValue || (me.Id == this.SenderId.Value) ? 0 : (this.Unread.HasValue ? this.Unread.Value : 0)
            };
        }

    }
}
