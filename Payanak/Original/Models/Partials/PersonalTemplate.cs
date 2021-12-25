using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class PersonalTemplate
    {
        public TemplateModel ToModel()
        {
            return new TemplateModel()
            {
                Id = this.Id,
                UserId = this.UserId.HasValue ? this.UserId.Value : -1,
                Body = this.Body,
                Title = this.Name
            };
        }
    }
}
