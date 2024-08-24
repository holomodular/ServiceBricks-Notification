using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods to add the ServiceBricks.Notification.EntityFrameworkCore module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks.Notification.EntityFrameworkCore module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationEntityFrameworkCoreModule), new NotificationEntityFrameworkCoreModule());

            // AI: Add the parent module
            services.AddServiceBricksNotification(configuration);

            // AI: Configure all options for the module

            // AI: Add any miscellaneous services for the module
            services.AddScoped<INotifyMessageProcessQueueService, NotifyMessageProcessQueueService>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiService>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiService>();

            // AI: Register business rules for the module
            DomainCreateUpdateDateRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance,
                nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return services;
        }
    }
}