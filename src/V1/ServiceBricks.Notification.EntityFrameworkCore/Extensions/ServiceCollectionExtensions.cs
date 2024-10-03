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
            // AI: Add the parent module
            services.AddServiceBricksNotification(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationEntityFrameworkCoreModule.Instance);

            // AI: Add module business rules
            NotificationEntityFrameworkCoreModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationEntityFrameworkCoreModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}