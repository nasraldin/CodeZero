using CodeZero;
using CodeZero.Domain.Common.Interfaces;
using CodeZero.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;
using WebAPI.Persistence;
using WebAPI.Persistence.Interceptors;
using WebAPI.Services;

namespace System;

/// <summary>
/// Custom Host Builder from <see cref="CodeZeroHostBuilder"/> 
/// with <see cref="ConfigurationBuilderOptions"/>.
/// </summary>
public static class CustomHostBuilder
{
    public static void CreateAsync(WebApplicationBuilder webApplication, string[] args)
    {
        // Example of configuring build options if you need that.
        var options = new ConfigurationBuilderOptions
        {
            BasePath = Directory.GetCurrentDirectory(),
            EnvironmentName = Environment.GetEnvironmentVariable(AppConst.ASPNETCORE_ENVIRONMENT)!,
            CommandLineArgs = args,
            UserSecretsId = "c78a43fb-0c6c-4b2e-bac8-b0d721cb7eff" // this from <UserSecretsId> prop in WebAPI.csproj
        };

        // Log Serilog Errors
        _ = bool.TryParse(webApplication.Configuration["DebugConfig:SerilogSelfLog"], out bool result);
        if (result && options.EnvironmentName == AppConst.Environments.Development)
        {
            SelfLog.Enable(Console.Error);
        }

        try
        {
            var builder = CodeZeroHostBuilder.CreateAsync(webApplication, options);

            // Add services to the container.
            // ...
            builder.Services.AddDomainServices();
            builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            if (builder.Configuration.GetValue<bool>("ServiceSettings:UseInMemoryDatabase"))
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("codezero_template"));
            }
            else
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    var folder = Environment.SpecialFolder.LocalApplicationData;
                    var path = Environment.GetFolderPath(folder);
                    options.UseSqlite($"Data Source={Path.Join(path, "codezero.db")}");
                });
            }

            builder.Services.AddScoped<IBaseDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            builder.Services.AddScoped<ApplicationDbContextInitialiser>();
            //builder.Services.AddTransient<IIdentityService, IdentityService>();
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

            //builder.Services.AddIdentityCore<ApplicationUser>();

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var app = builder.Build();
            app.UseCodeZero(builder.Configuration);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();

                // Initialise and seed database
                using (var scope = app.Services.CreateScope())
                {
                    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                    initialiser.InitialiseAsync().WaitAsync(CancellationToken.None);
                    initialiser.SeedAsync().WaitAsync(CancellationToken.None);
                }
            }

            // Configure the HTTP request pipeline.
            // ...

            app.Run();
        }
        catch (Exception ex)
        {
            // Log.Logger will likely be internal type "Serilog.Core.Pipeline.SilentLogger".
            if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
            {
                // Loading configuration or Serilog failed.
                // This will create a logger that can be captured by Azure logger.
                // To enable Azure logger, in Azure Portal:
                // 1. Go to WebApp
                // 2. App Service logs
                // 3. Enable "Application Logging (Filesystem)", "Application Logging (Filesystem)" and "Detailed error messages"
                // 4. Set Retention Period (Days) to 10 or similar value
                // 5. Save settings
                // 6. Under Overview, restart web app
                // 7. Go to Log Stream and observe the logs
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();
            }

            Log.Fatal(ex, "Host terminated unexpectedly.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Host terminated unexpectedly.");
            Console.WriteLine($"Exception: {ex}");
            Console.ResetColor();
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}