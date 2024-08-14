using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ServiceBricks.Notification.SendGrid
{
    /// <summary>
    /// The email provider service for SendGrid.
    /// </summary>
    public sealed class SendGridEmailProviderService : IEmailProvider
    {
        private readonly ILogger<SendGridEmailProviderService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        /// <summary>
        /// AppSetting key for the SendGrid API key.
        /// </summary>
        public const string APPSETTING_SENDGRID_APIKEY = @"ServiceBricks:Notification:SendGrid:ApiKey";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public SendGridEmailProviderService(
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<SendGridEmailProviderService>();
            _configuration = configuration;
            _apiKey = _configuration.GetValue<string>(APPSETTING_SENDGRID_APIKEY);
        }

        /// <summary>
        /// Sends an email using SendGrid.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IResponse> SendEmailAsync(NotifyMessageDto message)
        {
            Response resp = new Response();
            if (message == null)
            {
                _logger.LogError("Message Null");
                resp.AddMessage(ResponseMessage.CreateError("Error Sending Email"));
                return resp;
            }
            try
            {
                var client = new SendGridClient(_apiKey);
                var from = new EmailAddress(message.FromAddress);
                var subject = message.Subject;

                string[] toEmails;
                toEmails = GetEmails(message.ToAddress);
                EmailAddress to = new EmailAddress(toEmails[0]);

                var msg = MailHelper.CreateSingleEmail(from, to, subject, message.Body, message.BodyHtml);
                if (toEmails.Length > 1)
                {
                    for (int i = 1; i < toEmails.Length; i++)
                        msg.AddTo(toEmails[i]);
                }
                if (!string.IsNullOrEmpty(message.CcAddress))
                {
                    var list = GetEmails(message.CcAddress);
                    foreach (var item in list)
                        msg.AddCc(item);
                }
                if (!string.IsNullOrEmpty(message.BccAddress))
                {
                    var list = GetEmails(message.BccAddress);
                    foreach (var item in list)
                        msg.AddBcc(item);
                }

                var response = await client.SendEmailAsync(msg);
                if (response == null)
                {
                    resp.AddMessage(ResponseMessage.CreateError("Error Sending Email To Provider"));
                    return resp;
                }
                if (response.IsSuccessStatusCode)
                    return resp;

                _logger.LogError("SendGrid StatusCode {0} for storagekey {1}", response.StatusCode, message.StorageKey);
                resp.AddMessage(ResponseMessage.CreateError("Error Sending Email To Provider"));
                return resp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Sending Email {0}", message.StorageKey);
                resp.AddMessage(ResponseMessage.CreateError("Error Sending Email"));
                return resp;
            }
        }

        private string[] GetEmails(string source)
        {
            return source.Split(",");
        }
    }
}