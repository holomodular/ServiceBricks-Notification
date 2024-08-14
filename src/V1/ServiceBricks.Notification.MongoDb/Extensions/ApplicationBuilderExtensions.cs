using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// Extensions to start the ServiceBricks Notification MongoDb module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to check if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Notification MongoDb module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksNotificationMongoDb(this IApplicationBuilder applicationBuilder)
        {
            // AI: Flag the module as started
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksNotification();

            return applicationBuilder;
        }
    }
}