using System;
using System.Collections.Generic;
using Backend.ClientModels;

namespace Backend.Models
{
    public partial class PanelVersionInfo
    {
        public PanelVersionModel ToModel()
        {
            return new PanelVersionModel
            {
                Id = this.Id,
                CreateDate = this.CreateDate.HasValue ? this.CreateDate.Value.Ticks : -1,
                MaxVersion = this.MaxVersion.HasValue ? this.MaxVersion.Value : -1,
                MinVersion = this.MinVersion.HasValue ? this.MinVersion.Value : -1,
                Nickname = this.NickName,
                Path = this.Path
            };
        }
    }
}
