using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is a business rule for for the Message domain object.
    /// </summary>
    public partial class NotifyMessageRule : BusinessRule
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public NotifyMessageRule(ILoggerFactory loggerFactory, ITimezoneService timezoneService)
        {
            _logger = loggerFactory.CreateLogger<NotifyMessageRule>();
            Priority = PRIORITY_HIGH;
        }

        /// <summary>
        /// Register a rule for a domain object.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            //Generic object registration gets called for both create and update
            registry.RegisterItem(
                typeof(NotifyMessage),
                typeof(NotifyMessageRule));
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
                var domainObject = context.Object as NotifyMessage;
                if (domainObject == null)
                    return response;

                // Verify SenderTypeId
                var senderTypes = SenderType.GetAll();
                var found = senderTypes.Where(x => x.Key == domainObject.SenderTypeKey).FirstOrDefault();
                if (found == null)
                {
                    response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_ITEM_NOT_FOUND, nameof(NotifyMessage.SenderTypeKey)));
                    return response;
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