using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.SqlServer;

namespace ServiceBricks.Notification.SqlServer
{
    /// <summary>
    /// Extension methods for configuring the ServiceBricks Notification SqlServer module.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ServiceBricks Notification SqlServer module to the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(NotificationSqlServerModule.Instance);

            // AI: Add module business rules
            NotificationSqlServerModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<NotificationSqlServerModule>.Register(BusinessRuleRegistry.Instance);
            SqlServerDatabaseMigrationRule<NotificationSqlServerModule, NotificationSqlServerContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}