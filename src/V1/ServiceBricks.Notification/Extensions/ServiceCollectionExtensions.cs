using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotification(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationModule), new NotificationModule());

            // Configs
            services.Configure<NotificationOptions>(configuration.GetSection(NotificationConstants.APPSETTINGS_NOTIFICATION_OPTIONS));
            services.Configure<SmtpOptions>(configuration.GetSection(NotificationConstants.APPSETTINGS_SMTP_PROVIDER_OPTIONS));

            // Backgound Tasks
            services.AddHostedService<NotificationSendTimer>();
            services.AddScoped<NotificationSendTask.Worker>();

            // API Controllers
            services.AddScoped<INotifyMessageApiController, NotifyMessageApiController>();

            // Services
            var emailProvider = services.Where(x => x.ServiceType == typeof(IEmailProvider)).FirstOrDefault();
            if (emailProvider == null)
                services.AddScoped<IEmailProvider, ExampleEmailProvider>();
            var smsProvider = services.Where(x => x.ServiceType == typeof(ISmsProvider)).FirstOrDefault();
            if (smsProvider == null)
                services.AddScoped<ISmsProvider, ExampleSmsProvider>();

            // Business Rules
            SendNotificationProcessRule.RegisterRule(BusinessRuleRegistry.Instance);

            // ServiceBus Rules
            using (var serviceScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceBus = serviceScope.ServiceProvider.GetRequiredService<IServiceBus>();
                CreateApplicationEmailRule.RegisterServiceBus(serviceBus);
                CreateApplicationSmsRule.RegisterServiceBus(serviceBus);
            }
            return services;
        }

        public static IServiceCollection AddServiceBricksNotificationClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotifyMessageApiClient, NotifyMessageApiClient>();
            return services;
        }
    }
}