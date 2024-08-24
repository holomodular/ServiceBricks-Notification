using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is a business rule for NotifyMessage to make sure the dates are within bounds and stored in UTC zero.
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
                typeof(DomainUpdateBeforeEvent<NotifyMessage>),
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
                if (context.Object is DomainUpdateBeforeEvent<NotifyMessage> ei)
                {
                    var item = ei.DomainObject;

                    // THIS WILL BE REMOVED WITH NEW RULE
                    // AI: Check to make sure date is within bounds
                    if (item.ProcessDate < StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE)
                        item.ProcessDate = StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE;
                    if (item.FutureProcessDate < StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE)
                        item.FutureProcessDate = StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE;

                    // AI: Make sure we always store to UTC zero
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

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            return Task.FromResult(ExecuteRule(context));
        }
    }
}