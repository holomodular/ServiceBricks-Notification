using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;

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
            // AI: Add the parent module
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(new NotificationCosmosModule());

            // AI: Add module business rules
            NotificationCosmosModuleAddRule.Register(BusinessRuleRegistry.Instance);
            NotificationCosmosModuleStartRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationCosmosModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}