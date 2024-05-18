using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotificationMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationMongoDbModule), new NotificationMongoDbModule());

            // Add core
            services.AddServiceBricksNotification(configuration);

            // Configs
            services.Configure<NotificationOptions>(configuration.GetSection(nameof(NotificationOptions)));

            // Storage Services
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

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
            NotifyMessageUpdateRule.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return services;
        }
    }
}