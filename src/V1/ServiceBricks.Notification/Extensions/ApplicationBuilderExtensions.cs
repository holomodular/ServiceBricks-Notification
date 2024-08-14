using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// Extension methods for starting the ServiceBricks Notification module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to determine if the module has started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Notification module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksNotification(this IApplicationBuilder applicationBuilder)
        {
            // AI: Set the module started flag
            ModuleStarted = true;

            return applicationBuilder;
        }
    }
}