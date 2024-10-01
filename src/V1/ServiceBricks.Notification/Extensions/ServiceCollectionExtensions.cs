using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotification(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationModule.Instance);

            // AI: Add module business rules
            NotificationModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }

        /// <summary>
        /// Add the Notification Client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<INotifyMessageApiClient, NotifyMessageApiClient>();

            return services;
        }
    }
}