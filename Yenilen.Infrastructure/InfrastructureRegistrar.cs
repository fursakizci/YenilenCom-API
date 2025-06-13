using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Domain.Users;
using Yenilen.Infrastructure.DataAccess;
using Yenilen.Infrastructure.Options;
using Yenilen.Infrastructure.Services;

namespace Yenilen.Infrastructure;

public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            opt.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());

        services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 1;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.ConfigureOptions<JwtOptionsSetup>();
        //services.Configure<KeycloakConfiguration>(configuration.GetSection("KeycloakConfiguration"));
        
        //services.AddScoped<KeycloakService>();
        //services.AddScoped<IJwtProvider, KeycloakService>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            // Burada token validation parametrelerini de ekleyebilirsin.
            // options.TokenValidationParameters = new TokenValidationParameters { ... };

            // En önemli kısım burası:
            options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (string.IsNullOrEmpty(context.Token))
                    {
                        var accessToken = context.Request.Cookies["ACCESS_TOKEN"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                    }
                    return Task.CompletedTask;
                }
            };
        });
        
        
        
        services.AddAuthorization();
        //services.AddKeycloakWebApiAuthentication(configuration);
        //services.AddAuthorization().AddKeycloakAuthorization(configuration);
        
        services.Scan(opt => opt.
            FromAssemblies(typeof(InfrastructureRegistrar).Assembly) //dosya yoluna gore dependecylerin yolunu verir.
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)//dependecy injection uygulanmissa atla.
            .AsImplementedInterfaces()//interface ile implemente olanlari alma
            .WithScopedLifetime() //scoped atamis olduk.
        );
        
        return services;
    }
}