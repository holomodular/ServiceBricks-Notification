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
            // AI: Add the parent module
            services.AddServiceBricksNotification(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationMongoDbModule.Instance);

            // AI: Add module business rules
            NotificationMongoDbModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationMongoDbModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}