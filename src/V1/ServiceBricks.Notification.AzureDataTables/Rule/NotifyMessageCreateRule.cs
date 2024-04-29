using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is a business rule for the LogMessage object to set the
    /// partitionkey and rowkey of the object before create.
    /// </summary>
    public partial class NotifyMessageCreateRule : BusinessRule
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
        public NotifyMessageCreateRule(
            ILoggerFactory loggerFactory,
            ITimezoneService timezoneService)
        {
            _logger = loggerFactory.CreateLogger<NotifyMessageCreateRule>();
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
                typeof(NotifyMessageCreateRule));
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
                    // CreateDate is already in UTC format (due to previous rule)
                    var item = ei.DomainObject;
                    item.Key = Guid.NewGuid();

                    item.PartitionKey = item.CreateDate.ToString("yyyyMM");
                    var reverseDate = DateTimeOffset.MaxValue.Ticks - item.CreateDate.Ticks;
                    item.RowKey = reverseDate.ToString("d19") +
                        StorageAzureDataTablesConstants.KEY_DELIMITER +
                        item.Key.ToString();

                    // Check within bounds
                    if (item.ProcessDate < StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE)
                        item.ProcessDate = StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE;
                    if (item.FutureProcessDate < StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE)
                        item.FutureProcessDate = StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE;

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