using Microsoft.AspNetCore.Identity;
using Yenilen.Domain.Users;

namespace Yenilen.Infrastructure.Services;

public class IdentitySeeder
{
    public static async Task SeedRolesAsync(RoleManager<AppRole> roleManager)
    {
        string[] roles = { RoleNames.Customer, RoleNames.Staff, RoleNames.StoreOwner };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new AppRole(){Name = role});
            }
        }
    }
}