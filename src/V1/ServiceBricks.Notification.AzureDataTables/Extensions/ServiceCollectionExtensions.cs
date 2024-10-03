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
            // AI: Add parent module
            services.AddServiceBricksNotification(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationAzureDataTablesModule.Instance);

            // AI: Add module business rules
            NotificationAzureDataTablesModuleAddRule.Register(BusinessRuleRegistry.Instance);
            NotificationAzureDataTablesModuleStartRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationAzureDataTablesModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}