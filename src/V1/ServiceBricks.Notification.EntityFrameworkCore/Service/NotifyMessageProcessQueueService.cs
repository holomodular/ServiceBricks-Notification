using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// This is a service for processing a table like a queue.
    /// </summary>
    public sealed class NotifyMessageProcessQueueService : DomainProcessQueueService<NotifyMessage>, INotifyMessageProcessQueueService
    {
        private readonly IMapper _mapper;
        private readonly IBusinessRuleService _businessRuleService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        /// <param name="businessRuleService"></param>
        public NotifyMessageProcessQueueService(
            ILoggerFactory loggerFactory,
            IDomainProcessQueueStorageRepository<NotifyMessage> repo,
            IMapper mapper,
            IBusinessRuleService businessRuleService) : base(loggerFactory, repo)
        {
            _mapper = mapper;
            _businessRuleService = businessRuleService;
        }

        /// <summary>
        /// Process an item.
        /// </summary>
        /// <param name="domainObject"></param>
        public override async Task<IResponse> ProcessItemAsync(NotifyMessage domainObject)
        {
            var msg = _mapper.Map<NotifyMessageDto>(domainObject);
            SendNotificationProcess sendNotificationProcess = new SendNotificationProcess(msg);
            return await _businessRuleService.ExecuteProcessAsync(sendNotificationProcess);
        }

        /// <summary>
        /// Execute the process.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await base.ExecuteAsync(cancellationToken);
        }
    }
}