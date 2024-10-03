using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.InMemory
{
    /// <summary>
    /// Extension methods to add the ServiceBricks.Notification.InMemory module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks.Notification.InMemory module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationInMemoryModule.Instance);

            // AI: Add module business rules
            NotificationInMemoryModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationInMemoryModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}