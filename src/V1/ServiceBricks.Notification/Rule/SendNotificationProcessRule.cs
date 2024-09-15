using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This business rule occurs when an SendNotificationProcess is executed.
    /// </summary>
    public sealed class SendNotificationProcessRule : BusinessRule
    {
        private readonly IEmailProvider _emailProvider;
        private readonly ISmsProvider _smsProvider;
        private readonly ILogger<SendNotificationProcessRule> _logger;
        private readonly NotificationOptions _notificationOptions;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="emailProvider"></param>
        /// <param name="smsProvider"></param>
        /// <param name="notificationOptions"></param>
        /// <param name="mapper"></param>
        public SendNotificationProcessRule(
            ILoggerFactory loggerFactory,
            IEmailProvider emailProvider,
            ISmsProvider smsProvider,
            IOptions<NotificationOptions> notificationOptions,
            IMapper mapper
            )
        {
            _logger = loggerFactory.CreateLogger<SendNotificationProcessRule>();
            _emailProvider = emailProvider;
            _smsProvider = smsProvider;
            Priority = PRIORITY_NORMAL;
            _notificationOptions = notificationOptions.Value;
            _mapper = mapper;
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
        /// UnRegister the business rule.
        /// </summary>
        public static void UnRegisterRule(IBusinessRuleRegistry registry)
        {
            registry.UnRegisterItem(
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
                // AI: Make sure the context object is the correct type
                var p = context.Object as SendNotificationProcess;
                if (p == null || p.DomainObject == null)
                    return response;

                // Copy the object to avoid changing the original
                NotifyMessageDto msg = new NotifyMessageDto();
                msg = _mapper.Map(p.DomainObject, msg);

                // AI: Switch based on the sendertype and invoke the appropriate provider
                if (string.Compare(msg.SenderType, SenderType.Email_TEXT, true) == 0)
                {
                    // AI: Set defaults defined by options
                    if (!string.IsNullOrEmpty(_notificationOptions.EmailFromDefault))
                        msg.FromAddress = _notificationOptions.EmailFromDefault;
                    if (_notificationOptions.IsDevelopment)
                    {
                        msg.ToAddress = _notificationOptions.DevelopmentEmailTo;
                        msg.CcAddress = null;
                        msg.BccAddress = null;
                    }

                    // AI: Send the email
                    var respEmail = await _emailProvider.SendEmailAsync(msg);

                    // AI: Copy the response to the main response
                    response.CopyFrom(respEmail);
                    return response;
                }

                if (string.Compare(msg.SenderType, SenderType.SMS_TEXT, true) == 0)
                {
                    // AI: Set defaults defined by options
                    if (!string.IsNullOrEmpty(_notificationOptions.SmsFromDefault))
                        msg.FromAddress = _notificationOptions.SmsFromDefault;
                    if (_notificationOptions.IsDevelopment)
                    {
                        msg.ToAddress = _notificationOptions.DevelopmentSmsTo;
                        msg.CcAddress = null;
                        msg.BccAddress = null;
                    }

                    // AI: Send the SMS
                    var respSMS = await _smsProvider.SendSmsAsync(msg);

                    // AI: Copy the response to the main response
                    response.CopyFrom(respSMS);
                    return response;
                }

                // AI: Register new rules to process against the SendNotificationProcess via the IBusinessRuleRegistry

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