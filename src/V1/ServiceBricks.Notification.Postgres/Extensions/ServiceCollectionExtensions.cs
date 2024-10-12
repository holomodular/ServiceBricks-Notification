using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.Postgres;

namespace ServiceBricks.Notification.Postgres
{
    /// <summary>
    /// Extensions to add the ServiceBricks Notification Postgres module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Notification Postgres module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationPostgresModule.Instance);

            // AI: Add module business rules
            NotificationPostgresModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationPostgresModule>.Register(BusinessRuleRegistry.Instance);
            PostgresDatabaseMigrationRule<NotificationModule, NotificationPostgresContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}