using Microsoft.Extensions.Logging;
using ServiceBricks.Cache;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a work service for the notifymessagedto
    /// </summary>
    public partial class NotifyMessageWorkService : LockedWorkService<NotifyMessageDto>
    {
        protected readonly IBusinessRuleService _businessRuleService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="apiService"></param>
        /// <param name="businessRuleService"></param>
        /// <param name="semaphoreService"></param>
        public NotifyMessageWorkService(
            ILoggerFactory loggerFactory,
            IApiService<NotifyMessageDto> apiService,
            IBusinessRuleService businessRuleService,
            ISemaphoreService semaphoreService) : base(loggerFactory, apiService, semaphoreService)
        {
            _businessRuleService = businessRuleService;
            NumberToBatchProcess = 20;
        }

        /// <summary>
        /// Process the item.
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns></returns>
        public override async Task<IResponse> ProcessItemAsync(NotifyMessageDto dto)
        {
            SendNotificationProcess sendNotificationProcess = new SendNotificationProcess(dto);
            return await _businessRuleService.ExecuteProcessAsync(sendNotificationProcess);
        }
    }
}