using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is a business rule for the NotifyMessage object to set the key, partitionkey and rowkey of the object before create.
    /// It additionally makes sure dates are within bounds and are stored in UTC zero.
    /// </summary>
    public sealed class NotifyMessageCreateRule : BusinessRule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public NotifyMessageCreateRule()
        {
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(DomainCreateBeforeEvent<NotifyMessage>),
                typeof(NotifyMessageCreateRule));
        }

        /// <summary>
        /// Unregister the rule
        /// </summary>
        /// <param name="registry"></param>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
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

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var ei = context.Object as DomainCreateBeforeEvent<NotifyMessage>;
            if (ei == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

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

            return response;
        }
    }
}