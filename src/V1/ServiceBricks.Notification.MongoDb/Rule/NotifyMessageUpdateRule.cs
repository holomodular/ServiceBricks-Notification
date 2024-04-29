using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is a business rule for the LogMessage object to set the
    /// partitionkey and rowkey of the object before create.
    /// </summary>
    public partial class NotifyMessageUpdateRule : BusinessRule
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

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
                if (context.Object is DomainCreateBeforeEvent<NotifyMessage> ei)
                {
                    var item = ei.DomainObject;

                    // Convert each to valid UTC zero
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