using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class AccountInfo
    {
        public AccountInfo()
        {
            Group = new HashSet<Group>();
            NumberInfo = new HashSet<NumberInfo>();
            PersonalTemplate = new HashSet<PersonalTemplate>();
            UserGroups = new HashSet<UserGroups>();
            UserRoles = new HashSet<UserRoles>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string BusinessPhone { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLogin { get; set; }
        public string Picture { get; set; }
        public string FormGuid { get; set; }
        public DateTime? FormDate { get; set; }

        public virtual AdditionalInfo AdditionalInfo { get; set; }
        public virtual AddressInfo AddressInfo { get; set; }
        public virtual CreditInfo CreditInfo { get; set; }
        public virtual DeviceInfo DeviceInfo { get; set; }
        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual ICollection<Group> Group { get; set; }
        public virtual ICollection<NumberInfo> NumberInfo { get; set; }
        public virtual ICollection<PersonalTemplate> PersonalTemplate { get; set; }
        public virtual ICollection<UserGroups> UserGroups { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
