using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static void AddCommonServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public static IServiceCollection AddServiceBricksNotificationCosmos(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationCosmosModule), new NotificationCosmosModule());

            // Add Core
            services.AddServiceBricksNotification(configuration);

            // Register Database
            var builder = new DbContextOptionsBuilder<NotificationCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                NotificationCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(
                NotificationCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<NotificationCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationCosmosContext>>(builder.Options);
            services.AddDbContext<NotificationCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

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

            // Storage Services
            services.AddScoped<IStorageRepository<NotifyMessage>, MessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, MessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, MessageStorageRepository>();

            return services;
        }
    }
}