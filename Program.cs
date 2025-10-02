using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Resend;
using Serilog;
using Tienda.src.Application.Services.Implements;
using Tienda.src.Application.Services.Interfaces;
using Tienda.src.Domain.Models;
using Tienda.src.Infrastructure.Data;
using Tienda.src.Infrastructure.Middlewares;
using Tienda.src.Infrastructure.Repositories.Implements;
using Tienda.src.Infrastructure.Repositories.Interfaces;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

var connectionString = builder.Configuration.GetConnectionString("SqliteDatabase") ?? throw new InvalidOperationException("Connection string SqliteDatabase no configurado");


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();


# region Logging Configuration
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services));
#endregion


builder.Services.AddOpenApi();

#region Database Configuration
Log.Information("Configurando base de datos SQLite");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetSection("ConnectionStrings:SqliteDatabase").Value));
#endregion

#region Identity Configuration
Log.Information("Configurando Identity");
builder.Services.AddIdentityCore<User>(options =>
{
    //Configuración de contraseña
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;

    //Configuración de Email
    options.User.RequireUniqueEmail = true;

    //Configuración de UserName
    options.User.AllowedUserNameCharacters = builder.Configuration["IdentityConfiguration:AllowedUserNameCharacters"] ?? throw new InvalidOperationException("Los caracteres permitidos para UserName no están configurados.");
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
#endregion

builder.Services.AddControllers();

var app = builder.Build();

#region Database Migration and jobs Configuration
Log.Information("Aplicando migraciones a la base de datos");
using (var scope = app.Services.CreateScope())
{
    await DataSeeder.Initialize(scope.ServiceProvider);
}
#endregion

var _logger = app.Services.GetRequiredService<ILogger<Program>>();


var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application is starting...");

app.MapOpenApi();

app.MapControllers();

app.Run();