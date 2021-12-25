using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace artan_gym.Models
{
    public class UsersCoacheVW
    {
        public int Id { get; set; }
        public int? couchid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Coachname { get; set; }
        public string PhoneNumber { get; set; }
        //public string MelliCode { get; set; }
        public DateTime? dateSignIn { get; set; }
        //public DateTime contractlength { get; set; }
    }
}
