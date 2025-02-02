using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an example SMS Provider that writes messages to the log.
    /// </summary>
    public sealed class ExampleSmsProvider : ISmsProvider
    {
        private readonly ILogger _logger;

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
        public async Task<IResponse> SendSmsAsync(NotifyMessageDto message)
        {
            await Task.Delay(100); // Add small delay to simulate processing
            _logger.LogInformation("Sending SMS: " + JsonConvert.SerializeObject(message));
            return new Response();
        }
    }
}