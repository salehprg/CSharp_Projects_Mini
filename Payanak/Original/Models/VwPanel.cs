using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwPanel
    {
        public long? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastActivity { get; set; }
        public string Version { get; set; }
        public string Number { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public long? UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string GroupName { get; set; }
        public string GroupTitle { get; set; }
        public string GroupDescription { get; set; }
        public long? GroupId { get; set; }
        public string GroupPicture { get; set; }
        public string HashId { get; set; }
        public bool? IsBlocked { get; set; }
        public short? Status { get; set; }
        public long? TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateBody { get; set; }
        public long? NumberId { get; set; }
        public string SendNumber { get; set; }
        public bool? SendIsShared { get; set; }
        public bool? SendIsBlocked { get; set; }
        public short? SendType { get; set; }
        public string SendUsername { get; set; }
        public string SendPassword { get; set; }
        public string FormGuid { get; set; }
        public int? IsFormValid { get; set; }
    }
}
