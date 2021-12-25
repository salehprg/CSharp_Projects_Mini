using System;
using System.Collections.Generic;
using System.Collections;
using Backend.ClientModels;
using System.Linq;

namespace Backend.Models
{
    public partial class ReceiveInfo
    {
        public ReceiveSMSModel ToModel()
        {
            return new ReceiveSMSModel()
            {
                Body = this.Body,
                Count = this.Count.HasValue ? this.Count.Value : 0,
                MsgId = this.MsgId,
                Id = this.Id,
                Receiver = this.Receiver,
                Sender = this.Sender,
                SentDate = this.Date.HasValue ? this.Date.Value.Ticks : -1
            };
        }
    }
}
