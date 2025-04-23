using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Yenilen.Infrastructure.DataAccess;

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