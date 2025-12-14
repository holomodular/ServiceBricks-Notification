using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {       

        /// <summary>
        /// Add the Notification Client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<IApiClient<NotifyMessageDto>, NotifyMessageApiClient>();
            services.AddScoped<INotifyMessageApiClient, NotifyMessageApiClient>();

            return services;
        }

        /// <summary>
        /// Add the ServiceBricks Notification clients to the service collection for the API Service references
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationClientForService(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the API Services
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiClient>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiClient>();

            return services;
        }
    }
}