using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class PersonalTemplate
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public virtual AccountInfo User { get; set; }
    }
}
