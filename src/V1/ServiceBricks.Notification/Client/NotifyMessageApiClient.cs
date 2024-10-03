using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a REST API client for the NotifyMessageDto.
    /// </summary>
    public partial class NotifyMessageApiClient : ApiClient<NotifyMessageDto>, INotifyMessageApiClient
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public NotifyMessageApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(loggerFactory, httpClientFactory, configuration.GetApiConfig(NotificationConstants.APPSETTING_CLIENT_APICONFIG))
        {
            ApiResource = @"Notification/NotifyMessage";
        }
    }
}