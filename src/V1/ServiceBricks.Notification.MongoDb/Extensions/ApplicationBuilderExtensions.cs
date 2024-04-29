using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// IApplicationBuilder extensions for the Notification Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksNotificationMongoDb(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            // Start core
            applicationBuilder.StartServiceBricksNotification();

            return applicationBuilder;
        }
    }
}