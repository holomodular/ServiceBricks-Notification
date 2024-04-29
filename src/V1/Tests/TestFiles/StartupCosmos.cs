using Microsoft.AspNetCore.Hosting;
using ServiceBricks;
using ServiceBricks.Notification;
using ServiceBricks.Notification.Cosmos;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Xunit
{
    public class StartupCosmos : ServiceBricks.Startup
    {
        public StartupCosmos(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksNotificationCosmos(Configuration);

            // Remove all background tasks/timers for unit testing
            var sendtimer = services.Where(x => x.ImplementationType == typeof(NotificationSendTimer)).FirstOrDefault();
            if (sendtimer != null)
                services.Remove(sendtimer);

            // Register TestManager
            services.AddScoped<ITestManager<NotifyMessageDto>, NotifyMessageTestManager>();

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
            app.StartServiceBricksNotificationCosmos();
        }
    }
}