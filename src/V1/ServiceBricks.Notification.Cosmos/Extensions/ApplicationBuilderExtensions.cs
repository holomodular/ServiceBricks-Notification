using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// Extensions to start the ServiceBricks Notification Cosmos module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to check if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Notification Cosmos module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksNotificationCosmos(this IApplicationBuilder applicationBuilder)
        {
            // AI: Ensure the database is created
            using (var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NotificationCosmosContext>();
                context.Database.EnsureCreated();
            }

            // AI: Set the module started flag
            ModuleStarted = true;

            // AI: Start the parent module.
            applicationBuilder.StartServiceBricksNotificationEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}