using ServiceBricks;
using ServiceBricks.Notification.AzureDataTables;
using ServiceBricks.Cache.AzureDataTables;
using ServiceBricks.Logging.AzureDataTables;
using WebApp.Extensions;

namespace WebApp
{
    public class StartupAzureDataTables
    {
        public StartupAzureDataTables(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual IConfiguration Configuration { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksLoggingAzureDataTables(Configuration);
            services.AddServiceBricksCacheAzureDataTables(Configuration);
            services.AddServiceBricksNotificationAzureDataTables(Configuration);
            ModuleRegistry.Instance.Register(new WebApp.Model.WebAppModule()); // Just for automapper registration
            services.AddServiceBricksComplete(Configuration);
            services.AddCustomWebsite(Configuration);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            app.StartServiceBricks();
            app.StartCustomWebsite(webHostEnvironment);
            var logger = app.ApplicationServices.GetRequiredService<ILogger<StartupAzureDataTables>>();
            logger.LogInformation("Application Started");
        }
    }
}