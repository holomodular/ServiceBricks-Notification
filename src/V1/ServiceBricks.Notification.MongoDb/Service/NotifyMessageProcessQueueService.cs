using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is a service for processing a table like a queue.
    /// </summary>
    public class NotifyMessageProcessQueueService : DomainObjectProcessQueueService<NotifyMessage>, INotifyMessageProcessQueueService
    {
        protected readonly ILogger<NotifyMessageProcessQueueService> _logger;
        protected readonly IMapper _mapper;
        protected readonly IBusinessRuleService _businessRuleService;

        public NotifyMessageProcessQueueService(
            ILoggerFactory loggerFactory,
            IDomainObjectProcessQueueStorageRepository<NotifyMessage> repo,
            IMapper mapper,
            IBusinessRuleService businessRuleService) : base(loggerFactory, repo)
        {
            _logger = loggerFactory.CreateLogger<NotifyMessageProcessQueueService>();
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