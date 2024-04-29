using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace ServiceBricks.Notification
{
    public class NotifyMessageApiClient : ApiClient<NotifyMessageDto>, INotifyMessageApiClient
    {
        protected readonly IConfiguration _configuration;

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