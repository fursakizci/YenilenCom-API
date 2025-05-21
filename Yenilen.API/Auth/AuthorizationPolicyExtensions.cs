using Microsoft.AspNetCore.Authorization;
using Yenilen.Domain.Users;

namespace Yenilen.API.Auth;
public static class PolicyNames
{
    public const string RequireStoreOwner = "RequireStoreOwner";
    public const string RequireStoreOwnerOrStaff = "RequireStoreOwnerOrStaff";
    public const string RequireStoreAdministration = "RequireStoreAdministration";
}

public static class AuthorizationPolicyExtensions
{
    public static void AddYenilenPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(PolicyNames.RequireStoreOwner, policy => 
            policy.RequireRole(RoleNames.StoreOwner));
        
        options.AddPolicy(PolicyNames.RequireStoreOwnerOrStaff, policy => 
            policy.RequireRole(RoleNames.StoreOwner, RoleNames.Staff));
        
        options.AddPolicy(PolicyNames.RequireStoreAdministration, policy => 
            policy.RequireAssertion(context => 
                context.User.IsInRole(RoleNames.StoreOwner) || 
                (context.User.IsInRole(RoleNames.Staff) && 
                 context.User.HasClaim(c => c.Type == "StoreAdminPrivilege" && c.Value == "true"))));
    }
}

public class StoreAccessRequirement : IAuthorizationRequirement
{
    public string StoreId { get; }

    public StoreAccessRequirement(string storeId)
    {
        StoreId = storeId;
    }
}

public class StoreAccessHandler : AuthorizationHandler<StoreAccessRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StoreAccessRequirement requirement)
    {
        if (context.User.IsInRole(RoleNames.StoreOwner))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (context.User.IsInRole(RoleNames.Staff))
        {
            var storeAccessClaim = context.User.FindFirst(c =>
                c.Type == "StoreAccess" && c.Value == requirement.StoreId);

            if (storeAccessClaim != null)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}

