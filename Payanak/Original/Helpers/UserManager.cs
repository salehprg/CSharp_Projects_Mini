using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;

using Backend.Helpers.Permissionfolder;
using Backend.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Backend.Helpers
{
    public class UserManager {

        public static string GetUsername(ClaimsPrincipal _User)
        {
            var usernameClaim =_User.Claims.FirstOrDefault(x => x.Type == "username");
            if(usernameClaim != null)
            {
                return usernameClaim.Value;
            }

            return null;
        }
    }
}