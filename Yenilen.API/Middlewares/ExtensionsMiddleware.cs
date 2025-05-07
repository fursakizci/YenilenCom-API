using Microsoft.AspNetCore.Identity;
using Yenilen.Domain.Users;

namespace Yenilen.API.Middlewares;

public class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Taner",
                    LastName = "Saydam",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow 
                };

                user.CreateUserId = user.Id;
                
                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}