using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// Extensions to add the ServiceBricks Notification MongoDb module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Notification MongoDb module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationMongoDbModule), new NotificationMongoDbModule());

            // AI: Add the parent module
            services.AddServiceBricksNotification(configuration);

            // AI: Add any miscellaneous services for the module
            services.AddScoped<INotifyMessageProcessQueueService, NotifyMessageProcessQueueService>();

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiService>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiService>();

            // AI: Add business rules for the module
            DomainCreateUpdateDateRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance,
                nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.RegisterRule(BusinessRuleRegistry.Instance);

            NotifyMessageUpdateRule.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return services;
        }
    }
}