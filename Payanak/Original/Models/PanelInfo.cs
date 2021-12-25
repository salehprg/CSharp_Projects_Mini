using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class PanelInfo
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastActivity { get; set; }
        public string Version { get; set; }
        public string Number { get; set; }
        public string Serial { get; set; }
        public string HashId { get; set; }
        public string Name { get; set; }
        public long? GroupId { get; set; }
        public bool? IsBlocked { get; set; }
        public short? Status { get; set; }
        public long? TemplateId { get; set; }
        public long? NumberId { get; set; }
        public bool? HasForm { get; set; }
    }
}
