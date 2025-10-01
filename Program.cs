using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Resend;
using Serilog;
using Tienda.src.Domain.Models;
using Tienda.src.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

var connectionString = builder.Configuration.GetConnectionString("SqliteDatabase")
    ?? throw new InvalidOperationException("Connection string SqliteDatabase no configurado");

// Configurar Logging (Serilog)
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services));

// Configurar servicios
builder.Services.AddOpenApi();


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = builder.Configuration["IdentityConfiguration:AllowedUserNameCharacters"]
        ?? throw new InvalidOperationException("Los caracteres permitidos para UserName no están configurados.");
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// Construye la app después de configurar todos los servicios
var app = builder.Build();

// Ahora sí, usa los servicios del contenedor
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application is starting...");

app.MapOpenApi();

app.Run();