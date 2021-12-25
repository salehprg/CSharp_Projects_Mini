using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwBusinessCard
    {
        public long? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsBlocked { get; set; }
        public short? Status { get; set; }
        public long? UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? Birthday { get; set; }
        public short? Gender { get; set; }
        public short? Discount { get; set; }
        public decimal? Credit { get; set; }
        public long? GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupTitle { get; set; }
        public short? GroupStatus { get; set; }
        public string GroupPicture { get; set; }
        public long? TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateBody { get; set; }
        public long? NumberId { get; set; }
        public string NumberSend { get; set; }
        public bool? NumberIsShared { get; set; }
        public bool? NumberIsBlocked { get; set; }
        public long? NumberOwner { get; set; }
        public short? NumberType { get; set; }
        public string NumberUsername { get; set; }
        public string NumberPassword { get; set; }
        public string Key { get; set; }
        public string FormGuid { get; set; }
        public int? IsFormValid { get; set; }
    }
}
