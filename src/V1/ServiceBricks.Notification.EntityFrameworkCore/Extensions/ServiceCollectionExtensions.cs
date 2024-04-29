using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static void AddCommonServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public static IServiceCollection AddServiceBricksNotificationEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationEntityFrameworkCoreModule), new NotificationEntityFrameworkCoreModule());

            // Add Core
            services.AddServiceBricksNotification(configuration);

            // Configs
            services.Configure<NotificationOptions>(configuration.GetSection(nameof(NotificationOptions)));

            // Services
            services.AddScoped<INotifyMessageProcessQueueService, NotifyMessageProcessQueueService>();

            // API Services
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiService>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiService>();

            // Business Rules
            DomainCreateUpdateDateRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance,
                nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.RegisterRule(BusinessRuleRegistry.Instance);
            NotifyMessageDtoValidateSenderTypeRule.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return services;
        }
    }
}