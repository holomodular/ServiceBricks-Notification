using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods to start the ServiceBricks.Notification.EntityFrameworkCore module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to determine if the module has started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks.Notification.EntityFrameworkCore module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksNotificationEntityFrameworkCore(this IApplicationBuilder applicationBuilder)
        {
            // AI: Set the module started flag when complete
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksNotification();

            return applicationBuilder;
        }
    }
}