using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification.SendGrid
{
    /// <summary>
    /// Extension methods for configuring the ServiceBricks Notification SendGrid module.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ServiceBricks Notification SendGrid module to the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationSendGrid(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationSendGridModule.Instance);

            // AI: Add module business rules
            NotificationSendGridModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationSendGridModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}