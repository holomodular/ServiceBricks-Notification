using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// IApplicationBuilder extensions for the Notification Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksNotificationCosmos(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            using (var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NotificationCosmosContext>();
                context.Database.EnsureCreated();
            }

            // Start Core
            applicationBuilder.StartServiceBricksNotification();

            return applicationBuilder;
        }
    }
}