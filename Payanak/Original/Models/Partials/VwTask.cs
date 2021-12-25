using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwTask
    {
        public TaskResultModel ToModel()
        {
            return new TaskResultModel
            {
                Header = this.Header,
                Percent = this.Percent.HasValue ? this.Percent.Value : 100,
                Status = new List<ResponseStatusModel>{
                    new ResponseStatusModel{
                        status = this.Status.HasValue?this.Status.Value:0
                    }
                },
                UserId = this.UserId.Value
            };

        }
    }
}
