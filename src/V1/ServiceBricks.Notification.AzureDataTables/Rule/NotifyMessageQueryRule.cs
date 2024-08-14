using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is a business rule for the NotifyMessage when quering to change the storagekey to key.
    /// </summary>
    public sealed class NotifyMessageQueryRule : BusinessRule
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public NotifyMessageQueryRule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<NotifyMessageQueryRule>();
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register a rule for a domain object.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
                typeof(DomainQueryBeforeEvent<NotifyMessage>),
                typeof(NotifyMessageQueryRule));
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
                if (context.Object is DomainQueryBeforeEvent<NotifyMessage> ei)
                {
                    var item = ei.DomainObject;
                    if (ei.ServiceQueryRequest == null || ei.ServiceQueryRequest.Filters == null)
                        return response;

                    // AI: Check for the StorageKey filter and replace it with Key
                    foreach (var filter in ei.ServiceQueryRequest.Filters)
                    {
                        bool found = false;
                        if (filter.Properties != null &&
                            filter.Properties.Count > 0)
                        {
                            for (int i = 0; i < filter.Properties.Count; i++)
                            {
                                if (string.Compare(filter.Properties[i], "StorageKey", true) == 0)
                                {
                                    found = true;
                                    filter.Properties[i] = "Key";
                                }
                            }
                        }
                        if (found)
                        {
                            // AI: Iterate through the values. Split each one using the delimiter and re-set the value to use the second part.
                            if (filter.Values != null && filter.Values.Count > 0)
                            {
                                for (int i = 0; i < filter.Values.Count; i++)
                                {
                                    string[] split = filter.Values[i].Split(StorageAzureDataTablesConstants.KEY_DELIMITER);
                                    if (split.Length == 2)
                                    {
                                        filter.Values[i] = split[1];
                                    }
                                }
                            }
                        }
                    }
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