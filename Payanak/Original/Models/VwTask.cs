using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class VwTask
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public int? Percent { get; set; }
        public string Message { get; set; }
        public short? Status { get; set; }
        public string Header { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
