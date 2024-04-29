using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an example Email Provider.
    /// </summary>
    public partial class ExampleEmailProvider : IEmailProvider
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

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
        public virtual Task<IResponse> SendEmailAsync(NotifyMessageDto message)
        {
            _logger.LogInformation("Sending Email: " + JsonConvert.SerializeObject(message));
            return Task.FromResult<IResponse>(new Response());
        }
    }
}