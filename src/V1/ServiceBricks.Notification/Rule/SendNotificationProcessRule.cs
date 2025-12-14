using Microsoft.Extensions.Options;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This business rule occurs when an SendNotificationProcess is executed.
    /// </summary>
    public sealed class SendNotificationProcessRule : BusinessRule
    {
        private readonly IEmailProvider _emailProvider;
        private readonly ISmsProvider _smsProvider;
        private readonly SendOptions _sendOptions;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="emailProvider"></param>
        /// <param name="smsProvider"></param>
        /// <param name="sendOptions"></param>
        /// <param name="mapper"></param>
        public SendNotificationProcessRule(
            IEmailProvider emailProvider,
            ISmsProvider smsProvider,
            IOptions<SendOptions> sendOptions,
            IMapper mapper
            )
        {
            _emailProvider = emailProvider;
            _smsProvider = smsProvider;
            _sendOptions = sendOptions.Value;
            _mapper = mapper;
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register the business rule.
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(SendNotificationProcess),
                typeof(SendNotificationProcessRule));
        }

        /// <summary>
        /// UnRegister the business rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
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

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var p = context.Object as SendNotificationProcess;
            if (p == null || p.DomainObject == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Copy the object to avoid changing the original
            NotifyMessageDto msg = new NotifyMessageDto();
            msg = _mapper.Map(p.DomainObject, msg);

            // AI: Switch based on the sendertype and invoke the appropriate provider
            if (string.Compare(msg.SenderType, SenderType.Email_TEXT, true) == 0)
            {
                // AI: Set defaults defined by options
                if (!string.IsNullOrEmpty(_sendOptions.EmailFromDefault))
                    msg.FromAddress = _sendOptions.EmailFromDefault;
                if (_sendOptions.IsDevelopment)
                {
                    msg.ToAddress = _sendOptions.DevelopmentEmailTo;
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
                if (!string.IsNullOrEmpty(_sendOptions.SmsFromDefault))
                    msg.FromAddress = _sendOptions.SmsFromDefault;
                if (_sendOptions.IsDevelopment)
                {
                    msg.ToAddress = _sendOptions.DevelopmentSmsTo;
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
    }
}