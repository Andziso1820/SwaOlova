using SwaOlova.Application;
using SwaOlova.Infrastructure.Data;
//using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Interfaces.Services;
using SwaOlova.Infrastructure.Data.UnitOfWork;
using SwaOlova.Infrastructure.Service.Common;
using SwaOlova.Infrastructure.Service.Common.Interfaces;
using SwaOlova.Infrastructure.Service.Files;
using SwaOlova.Infrastructure.Service.Identity;
using SwaOlova.Infrastructure.Service.OTP;
using SwaOlova.Infrastructure.Service.Reporting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Identity;
using SwaOlova.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using SwaOlova.Portal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    var logFilePath = Path.Combine(context.HostingEnvironment.ContentRootPath, "Logs", "portal-.log");

    configuration
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "SwaOlova.Portal")
        .WriteTo.File(
            path: logFilePath,
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30,
            shared: true,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}");
});

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddApplication();

// Add DbContext with SQL Server provider
builder.Services.AddDbContext<SwaOlavaDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("SwaOlova.Infrastructure.Data");
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
    }));

builder.Services.AddInfrastructureDataLayer();
builder.Services.AddHttpContextAccessor();

// Add database initializer
builder.Services.AddScoped<DatabaseInitializer>();

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password requirements
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<SwaOlavaDbContext>()
.AddDefaultTokenProviders();

// Configure cookie authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ICodeGenerator, CodeGenerator>();
builder.Services.AddScoped<INumberGenerator, NumberGenerator>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IReportService, ReportService>();

var app = builder.Build();

// Initialize database
var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    using (var scope = app.Services.CreateScope())
    {
        logger.LogInformation("Starting database initialization...");
        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        await initializer.InitializeAsync();
        logger.LogInformation("Database initialization completed successfully.");
    }
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during database initialization. " +
        "Please ensure LocalDB is running: 'sqllocaldb start mssqllocaldb' " +
        "and database exists: 'sqlcmd -S (localdb)\\mssqllocaldb -Q \"CREATE DATABASE SwaOlavaDb\"'");

    // Still start the app - seeders can run on first request
    logger.LogWarning("Database initialization failed, but application will continue starting. " +
        "Database operations may fail until database is properly initialized.");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();

