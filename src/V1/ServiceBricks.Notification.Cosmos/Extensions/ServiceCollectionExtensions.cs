using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// Extensions to add the ServiceBricks Notification Cosmos module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Notification Cosmos module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationCosmos(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationCosmosModule), new NotificationCosmosModule());

            // AI: Add the parent module
            // AI: If the primary keys of the Cosmos models do not match the EFC module, we can't use EFC rules, so skip EFC and call start on the core module instead.
            services.AddServiceBricksNotification(configuration); // Skip EFC

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<NotificationCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                NotificationCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(
                NotificationCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<NotificationCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationCosmosContext>>(builder.Options);
            services.AddDbContext<NotificationCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Storage Services for the module for each domain object
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            // AI: Configure all options for the module
            services.Configure<NotificationOptions>(configuration.GetSection(nameof(NotificationOptions)));

            // AI: Register business rules for the module
            // AI: If the primary keys of the Cosmos models match the EFC module, we can use the EFC rules
            DomainCreateUpdateDateRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance,
                nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<NotifyMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            // AI: If the primary keys of the Cosmos models match the EFC module, we can use the EFC rules
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiService>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiService>();

            // AI: Add any miscellaneous services for the module
            services.AddScoped<INotifyMessageProcessQueueService, NotifyMessageProcessQueueService>();

            return services;
        }
    }
}