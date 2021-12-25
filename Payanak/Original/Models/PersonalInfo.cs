using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class PersonalInfo
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? Birthday { get; set; }
        public short? Gender { get; set; }
        public short? BirthdayChangeCounter { get; set; }
        public string NationalCode { get; set; }
        public bool? IsBirthdayChanged { get; set; }

        public virtual AccountInfo User { get; set; }
    }
}
