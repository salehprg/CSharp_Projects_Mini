using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class PanelVersionInfo
    {
        public long Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string NickName { get; set; }
        public string Path { get; set; }
        public decimal? MinVersion { get; set; }
        public decimal? MaxVersion { get; set; }
    }
}
