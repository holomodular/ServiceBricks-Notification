using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a provider for sending SMTP emails.
    /// </summary>
    public partial class SmtpEmailProvider : IEmailProvider
    {
        private readonly ILogger _logger;
        private readonly SmtpOptions _smtpOptions;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="eleflexNotificationOptions"></param>
        public SmtpEmailProvider(
            ILoggerFactory loggerFactory,
            IOptions<SmtpOptions> smtpOptions)
        {
            _logger = loggerFactory.CreateLogger<SmtpEmailProvider>();
            _smtpOptions = smtpOptions.Value;
        }

        /// <summary>
        /// Send an email.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual async Task<IResponse> SendEmailAsync(NotifyMessageDto message)
        {
            var response = new Response();
            try
            {
                SmtpClient client = new SmtpClient(_smtpOptions.EmailServer, _smtpOptions.EmailPort);
                client.EnableSsl = _smtpOptions.EmailEnableSsl;
                if (!string.IsNullOrEmpty(_smtpOptions.EmailUsername))
                    client.Credentials = new NetworkCredential(_smtpOptions.EmailUsername, _smtpOptions.EmailPassword);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(message.FromAddress);

                if (!string.IsNullOrEmpty(message.ToAddress))
                {
                    var list = GetEmails(message.ToAddress);
                    foreach (var item in list)
                        mailMessage.To.Add(item);
                }
                if (!string.IsNullOrEmpty(message.CcAddress))
                {
                    var list = GetEmails(message.CcAddress);
                    foreach (var item in list)
                        mailMessage.CC.Add(item);
                }
                if (!string.IsNullOrEmpty(message.BccAddress))
                {
                    var list = GetEmails(message.BccAddress);
                    foreach (var item in list)
                        mailMessage.Bcc.Add(item);
                }

                mailMessage.Body = message.Body;
                mailMessage.Subject = message.Subject;
                mailMessage.IsBodyHtml = message.IsHtml;
                if (message.IsHtml && !string.IsNullOrEmpty(message.BodyHtml))
                    mailMessage.Body = message.BodyHtml;

                if (!string.IsNullOrEmpty(message.Priority))
                {
                    if (string.Compare(message.Priority, MailPriority.High.ToString(), true) == 0)
                        mailMessage.Priority = MailPriority.High;
                    else if (string.Compare(message.Priority, MailPriority.Low.ToString(), true) == 0)
                        mailMessage.Priority = MailPriority.Low;
                    else
                        mailMessage.Priority = MailPriority.Normal;
                }

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError("Error sending email."));
            }
            return response;
        }

        /// <summary>
        /// Get emails.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected virtual string[] GetEmails(string source)
        {
            return source.Split(",");
        }
    }
}