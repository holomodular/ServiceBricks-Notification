using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an example SMS Provider.
    /// </summary>
    public partial class ExampleSmsProvider : ISmsProvider
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ExampleSmsProvider(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExampleSmsProvider>();
        }

        /// <summary>
        /// Send an SMS message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual Task<IResponse> SendSmsAsync(NotifyMessageDto message)
        {
            _logger.LogInformation("Sending SMS: " + JsonConvert.SerializeObject(message));
            return Task.FromResult<IResponse>(new Response());
        }
    }
}