using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotificationAzureDataTables(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationAzureDataTablesModule), new NotificationAzureDataTablesModule());

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
            DomainDateTimeOffsetRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance, nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.RegisterRule(BusinessRuleRegistry.Instance);
            NotifyMessageRule.RegisterRule(BusinessRuleRegistry.Instance);
            NotifyMessageCreateRule.RegisterRule(BusinessRuleRegistry.Instance);
            NotifyMessageUpdateRule.RegisterRule(BusinessRuleRegistry.Instance);
            NotifyMessageQueryRule.RegisterRule(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}