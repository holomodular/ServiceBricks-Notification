using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotification(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationModule), new NotificationModule());

            // AI: Add any custom requirements for the module

            // AI: Add hosted services for the module
            services.AddHostedService<NotificationSendTimer>();

            // AI: Add workers for tasks in the module
            services.AddScoped<NotificationSendTask.Worker>();

            // AI: Configure all options for the module
            services.Configure<NotificationOptions>(configuration.GetSection(NotificationConstants.APPSETTINGS_NOTIFICATION_OPTIONS));
            services.Configure<SmtpOptions>(configuration.GetSection(NotificationConstants.APPSETTINGS_SMTP_PROVIDER_OPTIONS));

            // AI: Add API Controllers for each DTO in the module
            services.AddScoped<INotifyMessageApiController, NotifyMessageApiController>();

            // AI: Add any miscellaneous services for the module
            var emailProvider = services.Where(x => x.ServiceType == typeof(IEmailProvider)).FirstOrDefault();
            if (emailProvider == null)
                services.AddScoped<IEmailProvider, ExampleEmailProvider>();
            var smsProvider = services.Where(x => x.ServiceType == typeof(ISmsProvider)).FirstOrDefault();
            if (smsProvider == null)
                services.AddScoped<ISmsProvider, ExampleSmsProvider>();

            // AI: Register business rules for the module
            SendNotificationProcessRule.Register(BusinessRuleRegistry.Instance);

            // AI: Register servicebus subscriptions for the module
            using (var serviceScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceBus = serviceScope.ServiceProvider.GetRequiredService<IServiceBus>();
                CreateApplicationEmailRule.Register(serviceBus);
                CreateApplicationSmsRule.Register(serviceBus);
            }

            return services;
        }

        /// <summary>
        /// Add the Notification Client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<INotifyMessageApiClient, NotifyMessageApiClient>();

            return services;
        }
    }
}