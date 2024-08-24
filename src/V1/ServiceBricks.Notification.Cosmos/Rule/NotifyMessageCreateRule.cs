using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is a business rule for creating a NotifyMessage domain object. It will set the Key and PartitionKey.
    /// </summary>
    public sealed class NotifyMessageCreateRule : BusinessRule
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public NotifyMessageCreateRule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<NotifyMessageCreateRule>();
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register the business rule to the DomainCreateBeforeEvent.
        /// </summary>
        /// <param name="registry"></param>
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
                if (context.Object is DomainCreateBeforeEvent<NotifyMessage> e)
                {
                    // AI: Set the Key and PartitionKey
                    var item = e.DomainObject;
                    item.Key = Guid.NewGuid();

                    // AI: Set the PartitionKey to be the year, month and day so that the data is partitioned
                    item.PartitionKey = item.CreateDate.ToString("yyyyMMdd");
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
            // AI: There is no async work, so just call the sync method
            return Task.FromResult<IResponse>(ExecuteRule(context));
        }
    }
}