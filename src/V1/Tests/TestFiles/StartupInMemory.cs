using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification;
using ServiceBricks.Notification.InMemory;
using ServiceBricks.Cache.InMemory;

namespace ServiceBricks.Xunit
{
    public class StartupInMemory : ServiceBricks.Startup
    {
        public StartupInMemory(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksCacheInMemory(Configuration);
            services.AddServiceBricksNotificationInMemory(Configuration);
            services.AddServiceBricksComplete(Configuration);

            // Remove all background tasks/timers for unit testing
            var logtimer = services.Where(x => x.ImplementationType == typeof(SendNotificationTimer)).FirstOrDefault();
            if (logtimer != null)
                services.Remove(logtimer);

            // Register TestManager
            services.AddScoped<ITestManager<NotifyMessageDto>, NotifyMessageTestManager>();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
        }
    }
}