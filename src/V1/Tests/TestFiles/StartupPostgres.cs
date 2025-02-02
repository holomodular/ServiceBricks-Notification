using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification;
using ServiceBricks.Notification.Postgres;
using ServiceBricks.Cache.Postgres;

namespace ServiceBricks.Xunit
{
    public class StartupPostgres : ServiceBricks.Startup
    {
        public StartupPostgres(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksCachePostgres(Configuration);
            services.AddServiceBricksNotificationPostgres(Configuration);
            services.AddServiceBricksComplete(Configuration);

            // Remove all background tasks/timers for unit testing
            var timer = services.Where(x => x.ImplementationType == typeof(SendNotificationTimer)).FirstOrDefault();
            if (timer != null)
                services.Remove(timer);

            // Register TestManager
            services.AddScoped<ITestManager<NotifyMessageDto>, NotifyMessageTestManagerPostgres>();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
        }
    }
}