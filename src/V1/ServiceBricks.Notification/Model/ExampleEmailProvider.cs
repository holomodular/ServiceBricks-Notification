using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an example Email Provider that writes messages to the log.
    /// </summary>
    public sealed class ExampleEmailProvider : IEmailProvider
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ExampleEmailProvider(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExampleEmailProvider>();
        }

        /// <summary>
        /// Send an email.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IResponse> SendEmailAsync(NotifyMessageDto message)
        {
            await Task.Delay(100); // Add small delay to simulate processing
            _logger.LogInformation("Sending Email: " + JsonConvert.SerializeObject(message));
            return new Response();
        }
    }
}