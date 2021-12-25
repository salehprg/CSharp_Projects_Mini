using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Models.User
{
    
    public class UserModel : IdentityUser<int> {
        

        public int couchid {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}

        [NotMapped]
        public string Password {get;set;}
    }

}