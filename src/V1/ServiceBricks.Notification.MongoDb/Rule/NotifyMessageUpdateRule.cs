using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is a business rule for the NotifyMessage object to make sure dates are in the correct format.
    /// </summary>
    public sealed class NotifyMessageUpdateRule : BusinessRule
    {
        private readonly ILogger _logger;
        private readonly ITimezoneService _timezoneService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public NotifyMessageUpdateRule(
            ILoggerFactory loggerFactory,
            ITimezoneService timezoneService)
        {
            _logger = loggerFactory.CreateLogger<NotifyMessageUpdateRule>();
            _timezoneService = timezoneService;
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register a rule for a domain object.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
                typeof(DomainCreateBeforeEvent<NotifyMessage>),
                typeof(NotifyMessageUpdateRule));
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResponse ExecuteRule(IBusinessRuleContext context)
        {
            var response = new Response();

            try
            {
                // AI: Make sure the context object is the correct type
                if (context.Object is DomainCreateBeforeEvent<NotifyMessage> ei)
                {
                    var item = ei.DomainObject;

                    // AI: Convert each to valid UTC zero
                    if (item.ProcessDate.Offset != TimeSpan.Zero)
                        item.ProcessDate = _timezoneService.ConvertPostBackToUTC(item.ProcessDate);
                    if (item.FutureProcessDate.Offset != TimeSpan.Zero)
                        item.FutureProcessDate = _timezoneService.ConvertPostBackToUTC(item.FutureProcessDate);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
            }

            return response;
        }
    }
}