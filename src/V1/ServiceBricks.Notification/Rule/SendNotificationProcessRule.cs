using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This business rule occurs when an email needs to be sent.
    /// </summary>
    public partial class SendNotificationProcessRule : BusinessRule
    {
        private readonly IEmailProvider _emailProvider;
        private readonly ISmsProvider _smsProvider;
        private readonly ILogger<SendNotificationProcessRule> _logger;
        private readonly NotificationOptions _notificationOptions;

        public SendNotificationProcessRule(
            ILoggerFactory loggerFactory,
            IEmailProvider emailProvider,
            ISmsProvider smsProvider,
            IOptions<NotificationOptions> notificationOptions)
        {
            _logger = loggerFactory.CreateLogger<SendNotificationProcessRule>();
            _emailProvider = emailProvider;
            _smsProvider = smsProvider;
            Priority = PRIORITY_NORMAL;
            _notificationOptions = notificationOptions.Value;
        }

        /// <summary>
        /// Register the business rule.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
                typeof(SendNotificationProcess),
                typeof(SendNotificationProcessRule));
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResponse ExecuteRule(IBusinessRuleContext context)
        {
            return ExecuteRuleAsync(context).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            var response = new Response();

            try
            {
                var p = context.Object as SendNotificationProcess;
                if (p == null || p.DomainObject == null)
                    return response;

                var msg = p.DomainObject;

                switch (p.DomainObject.SenderTypeKey)
                {
                    case SenderType.Email:

                        // Set defaults defined by options
                        if (!string.IsNullOrEmpty(_notificationOptions.EmailFromDefault))
                            msg.FromAddress = _notificationOptions.EmailFromDefault;
                        if (_notificationOptions.IsDevelopment)
                        {
                            msg.ToAddress = _notificationOptions.DevelopmentEmailTo;
                            msg.CcAddress = null;
                            msg.BccAddress = null;
                        }

                        var respEmail = await _emailProvider.SendEmailAsync(msg);
                        response.CopyFrom(respEmail);
                        break;

                    case SenderType.SMS:

                        // Set defaults defined by options
                        if (!string.IsNullOrEmpty(_notificationOptions.SmsFromDefault))
                            msg.FromAddress = _notificationOptions.SmsFromDefault;
                        if (_notificationOptions.IsDevelopment)
                        {
                            msg.ToAddress = _notificationOptions.DevelopmentSmsTo;
                            msg.CcAddress = null;
                            msg.BccAddress = null;
                        }

                        var respSMS = await _smsProvider.SendSmsAsync(msg);
                        response.CopyFrom(respSMS);
                        break;

                    default:
                        _logger.LogError($"SenderTypeKey not defined for {msg.SenderTypeKey}");
                        break;
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
                return response;
            }
        }
    }
}