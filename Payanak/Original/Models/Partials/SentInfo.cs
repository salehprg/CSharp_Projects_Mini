using System;
using System.Collections.Generic;
using System.Collections;
using Backend.ClientModels;
using System.Linq;

namespace Backend.Models
{
    public partial class SentInfo
    {
        public SentSMSModel ToModel()
        {
            return new SentSMSModel()
            {
                Body = this.Body,
                Count = this.CalculatedCount.HasValue ? this.CalculatedCount.Value : 0,
                Delivery = null,
                GroupIds = this.GroupIds?.Split(";").Select(a => long.Parse(a)).ToList(),
                Header = this.Header,
                Numbers = this.Numbers?.Split(";").ToList(),
                Price = this.Price.HasValue ? this.Price.Value : 0,
                SendNumber = this.SendNumber,
                SentDate = this.SentDate.Value.Ticks,
                Status = this.Status.HasValue ? this.Status.Value : (short)0,
            };
        }
    }
}
