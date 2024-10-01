using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.Sqlite;

namespace ServiceBricks.Notification.Sqlite
{
    /// <summary>
    /// Extensions to add the ServiceBricks Notification Sqlite module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Notification Sqlite module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(new NotificationSqliteModule());

            // AI: Add module business rules
            NotificationSqliteModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationSqliteModule>.Register(BusinessRuleRegistry.Instance);
            SqliteDatabaseMigrationRule<NotificationSqliteModule, NotificationSqliteContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}