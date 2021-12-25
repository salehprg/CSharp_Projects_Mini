using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwContactGroups
    {
        public long? Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? SpecialDate { get; set; }
        public string InstagramLink { get; set; }
        public string TelegramLink { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? Birthday { get; set; }
        public short? Gender { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public long? Owner { get; set; }
        public short? Status { get; set; }
        public long? GroupId { get; set; }
        public string FormGuid { get; set; }
        public int? IsFormValid { get; set; }
    }
}
