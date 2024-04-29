using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// This is a business rule for for the Message domain object.
    /// </summary>
    public partial class NotifyMessageDtoValidateSenderTypeRule : BusinessRule
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public NotifyMessageDtoValidateSenderTypeRule(ILoggerFactory loggerFactory, ITimezoneService timezoneService)
        {
            _logger = loggerFactory.CreateLogger<NotifyMessageDtoValidateSenderTypeRule>();
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register a rule for a domain object.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            //Generic object registration gets called for both create and update
            registry.RegisterItem(
                typeof(NotifyMessageDto),
                typeof(NotifyMessageDtoValidateSenderTypeRule));
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
                var domainObject = context.Object as NotifyMessageDto;
                if (domainObject == null)
                    return response;

                // Verify SenderType
                var senderTypes = SenderType.GetAll();
                var found = senderTypes.Where(x => x.Key == domainObject.SenderTypeKey).FirstOrDefault();
                if (found == null)
                {
                    response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_ITEM_NOT_FOUND, nameof(NotifyMessageDto.SenderTypeKey)));
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