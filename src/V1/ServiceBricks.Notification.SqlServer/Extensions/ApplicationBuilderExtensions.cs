using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.SqlServer
{
    /// <summary>
    /// Extensions for configuring the ServiceBricks Notification SqlServer module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Starts the ServiceBricks Notification SqlServer module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksNotificationSqlServer(this IApplicationBuilder applicationBuilder)
        {
            // AI: Migrate the database
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<NotificationSqlServerContext>();
                context.Database.Migrate();
                context.SaveChanges();
            }

            // AI: Set the module started flag
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksNotificationEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}