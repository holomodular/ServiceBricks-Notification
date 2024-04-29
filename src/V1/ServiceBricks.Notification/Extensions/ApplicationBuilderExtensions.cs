using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// IApplicationBuilder extensions for the Notification Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksNotification(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;
            return applicationBuilder;
        }
    }
}