using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Helpers.Permissionfolder
{
    public static class Permissions
    {
        public static class Dashboards
        {
            public const string View = "Permissions.1";
            public const string Create = "Permissions.2";
            public const string Edit = "Permissions.3";
            public const string Delete = "Permissions.Dashboards.Delete";
            public const string Manage = "Permissions.Dashboards.Manage";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }
    }
}
