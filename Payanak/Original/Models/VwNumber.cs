using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwNumber
    {
        public long? Id { get; set; }
        public string Number { get; set; }
        public bool? IsShared { get; set; }
        public bool? IsBlocked { get; set; }
        public long? Owner { get; set; }
        public short? Type { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public string OwnerUsername { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short? Gender { get; set; }
        public string Address { get; set; }
        public string FormGuid { get; set; }
        public int? IsFormValid { get; set; }
    }
}
