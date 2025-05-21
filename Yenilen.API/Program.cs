using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Yenilen.API.Auth;
using Yenilen.API.Middlewares;
using Yenilen.Application.Common.Mapping;
using Yenilen.Infrastructure;
using Yenilen.Application;
using Yenilen.Domain.Users;
using Yenilen.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
}); // veriyi sikistirmak icin.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(
                    "https://yenilen-com-client.vercel.app",
                    "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Yenilen.API", Version = "v1" });

    // 🔐 JWT Bearer token konfigürasyonu
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header. Örn: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization(options =>
{
    // Add the custom policies
    options.AddYenilenPolicies();
});
// Register authorization handler
builder.Services.AddSingleton<IAuthorizationHandler, StoreAccessHandler>();



builder.Services.AddAutoMapper(typeof(CustomerMappingProfile)); // added AutoMapper service
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

//Mediatr service
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly));
builder.Services.AddApplication();
builder.Services.AddRateLimiter(x =>
    x.AddFixedWindowLimiter("fixed", cfg =>
    {
        cfg.QueueLimit = 100;
        cfg.Window = TimeSpan.FromSeconds(1);
        cfg.PermitLimit = 100;
        cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }));

//builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();


var app = builder.Build();

//Seed class program ilk calistigin rolleri atamak icin db ye.
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    await IdentitySeeder.SeedRolesAsync(roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // HTTPS ile iletilen verileri sifreler  UseResponseCompression dan once olmali

app.UseCors("AllowFrontend");

//Authentication ve authorization app ayarlari.
app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression(); // responselari sıkistiriyor.

app.UseGlobalExceptionHandling();

app.MapControllers().RequireRateLimiting("fixed");

//ExtensionsMiddleware.CreateFirstUser(app);

app.Run();

