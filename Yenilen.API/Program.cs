using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Yenilen.API.Middlewares;
using Yenilen.API.Shared;
using Yenilen.Application.Common.Mapping;
using Yenilen.Infrastructure;
using Yenilen.Application;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(UserMappingProfile)); // added AutoMapper service
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseGlobalExceptionHandling();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();

