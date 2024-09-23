using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// Extension methods for adding the ServiceBricks Notification Azure Data Tables module to the DI container.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Notification Azure Data Tables module to the DI container.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationAzureDataTables(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationAzureDataTablesModule), new NotificationAzureDataTablesModule());

            // AI: Add parent module
            services.AddServiceBricksNotification(configuration);

            // AI: Configure all options for the module

            // AI: Add storage services for the module. Each domain object should have its own storage repository
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiService>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiService>();

            // AI: Add any miscellaneous services for the module
            services.AddScoped<INotifyMessageProcessQueueService, NotifyMessageProcessQueueService>();

            // AI: Register business rules for the module
            DomainCreateUpdateDateRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance, nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.Register(BusinessRuleRegistry.Instance);
            NotifyMessageCreateRule.Register(BusinessRuleRegistry.Instance);
            NotifyMessageUpdateRule.Register(BusinessRuleRegistry.Instance);
            NotifyMessageQueryRule.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}