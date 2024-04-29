using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// IApplicationBuilder extensions for the Notification Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksNotificationEntityFrameworkCore(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            // Start Core
            applicationBuilder.StartServiceBricksNotification();

            return applicationBuilder;
        }
    }
}