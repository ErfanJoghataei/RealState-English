// FILE: Program.cs
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealState.BusinessLogik.Servises;
using RealState.BusinessLogik.Servises.AdminServises;
using RealState.Dal.Contexs;
using RealState.Middleware; // LOG ADDED - for middlewares
using RealState.UI.Middleware;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using System;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Serilog setup --------------------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // allow override from appsettings.json
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
    .CreateLogger();

builder.Host.UseSerilog(); // LOG ADDED
// ------------------------------------------------------
// اضافه کردن لاگ به فایل
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile("Logs/efcore-log-{Date}.txt"); // مسیر و نام فایل لاگ
var cnnstring = builder.Configuration.GetConnectionString("DefaultConnection");
// DbContext با لاگینگ
// اضافه کردن DbContext با لاگ به فایل
builder.Services.AddDbContext<RealStateDbContext>((serviceProvider, options) =>
{
    var loggerFactory = LoggerFactory.Create(logging =>
    {
        logging.AddFile("Logs/efcore-log-{Date}.txt");
        logging.SetMinimumLevel(LogLevel.Information);
    });

    options.UseLoggerFactory(loggerFactory)
           .UseSqlServer(cnnstring) // فقط از cnnstring استفاده کنید
           .EnableSensitiveDataLogging() // نمایش داده‌های حساس برای توسعه
           .LogTo(Console.WriteLine, LogLevel.Information);
});
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // LOG ADDED (used by some logging scenarios)

    builder.Services.AddDbContext<RealStateDbContext>(options =>
    {
        options.UseSqlServer(cnnstring)
               .EnableSensitiveDataLogging() // نمایش داده‌های حساس در لاگ (برای توسعه)
               .LogTo(Console.WriteLine, LogLevel.Information); // یا به ILogger بفرست
    });


    builder.Services.AddScoped<IRealStateServise, RealStateServise>();
    builder.Services.AddScoped<IAdminService, AdminService>();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true; // جاوااسکریپت به کوکی دسترسی ندارد
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // فقط HTTPS
            options.LoginPath = "/Login/Login";

            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // مدت نشست
        });
    builder.Services.AddDistributedMemoryCache(); // حافظه موقت برای Session
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30); // مدت زمان اعتبار Session
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    builder.Services.AddAuthorization();

    var app = builder.Build();

    // -------------------- Logging middlewares (important order) --------------------
    app.UseMiddleware<ExceptionHandlingMiddleware>(); // LOG ADDED - global exception catcher & logger
    app.UseMiddleware<CorrelationIdMiddleware>(); // LOG ADDED - assign/propagate correlation id
    app.UseMiddleware<ExceptionLoggingMiddleware>();
    app.UseSerilogRequestLogging(); // LOG ADDED - logs HTTP request/response timing
                                    // ---------------------------------------------------------------------------


    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error"); // optional fallback
        app.UseHsts();
        app.UseHttpsRedirection();
    }
    app.UseRouting();
    app.UseSession();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseStaticFiles();
    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
     .WithStaticAssets();

    if (app.Environment.IsDevelopment())
    {
        // The copied project uses its own demo database. The inherited migration
        // chain cannot build a new database from scratch, so create the current
        // model directly for local development.
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<RealStateDbContext>();
        db.Database.EnsureCreated();
        foreach (var listing in RealState.UI.Models.ShowcaseProperties.All)
        {
            if (!db.properties.Any(p => p.Code == listing.Code))
            {
                db.properties.Add(RealState.UI.Models.ShowcaseProperties.ToDatabaseCopy(listing));
            }
        }
        db.SaveChanges();
    }

    try
    {
        Log.Information("Starting web host"); // LOG ADDED
        app.Run();
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Host terminated unexpectedly"); // LOG ADDED
    }
    finally
    {
        Log.CloseAndFlush(); // LOG ADDED
    }



