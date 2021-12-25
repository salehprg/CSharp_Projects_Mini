using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;

using Backend.Helpers.Permissionfolder;
using Backend.Models;
using Backend.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly SmsPanelDbContext _context;

    public PermissionAuthorizationHandler(SmsPanelDbContext applicationDbContext)
    {
        _context = applicationDbContext;

    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var username = UserManager.GetUsername(context.User);
        if(username == null)
        {
            context.Fail();
            return;
        }
        
        var userAcc = _context.AccountInfo.Where (a => a.Username == username)
                    .Include (a => a.UserRoles)
                    .ThenInclude (a => a.Role)
                    .ThenInclude (a => a.RolePermissions)
                    .ThenInclude (a => a.Permission)
                    .FirstOrDefault ();

        var userPermissions = userAcc.UserRoles
            .Select (a => a.Role.RolePermissions
                .Select (b => b.Permission))
            .SelectMany (a => a)
            .ToList ();

        
        if(userPermissions.Any(a => a.Name == requirement.Permission))
        {
            context.Succeed(requirement);
            return;
        }

    }
}