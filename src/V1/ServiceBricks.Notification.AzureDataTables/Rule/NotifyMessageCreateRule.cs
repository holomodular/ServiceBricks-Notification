using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is a business rule for the NotifyMessage object to set the key, partitionkey and rowkey of the object before create.
    /// It additionally makes sure dates are within bounds and are stored in UTC zero.
    /// </summary>
    public sealed class NotifyMessageCreateRule : BusinessRule
    {
        private readonly ILogger _logger;
        private readonly ITimezoneService _timezoneService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public NotifyMessageCreateRule(
            ILoggerFactory loggerFactory,
            ITimezoneService timezoneService)
        {
            _timezoneService = timezoneService;
            _logger = loggerFactory.CreateLogger<NotifyMessageCreateRule>();
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
                // AI: Make sure the context object is the correct type
                if (context.Object is DomainCreateBeforeEvent<NotifyMessage> ei)
                {
                    // AI: Set the Key, PartitionKey, and RowKey
                    // AI: CreateDate is already in UTC format (now - due to previous rule)
                    var item = ei.DomainObject;
                    item.Key = Guid.NewGuid();

                    // AI: Set the PartitionKey to be the year, month and day so that the data is partitioned
                    item.PartitionKey = item.CreateDate.ToString("yyyyMMdd");

                    // AI: Set the RowKey to be the reverse date and time so that the newest items are at the top when querying
                    var reverseDate = DateTimeOffset.MaxValue.Ticks - item.CreateDate.Ticks;
                    item.RowKey = reverseDate.ToString("d19") +
                        StorageAzureDataTablesConstants.KEY_DELIMITER +
                        item.Key.ToString();

                    // TODO: THIS WILL BE REMOVED WITH NEW RULE
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
    }
}