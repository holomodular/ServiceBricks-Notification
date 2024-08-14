using AutoMapper;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is a REST API service for the NotifyMessageDto.
    /// </summary>
    public partial class NotifyMessageApiService : ApiService<NotifyMessage, NotifyMessageDto>, INotifyMessageApiService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="businessRuleService"></param>
        /// <param name="repository"></param>
        public NotifyMessageApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<NotifyMessage> repository)
            : base(mapper, businessRuleService, repository)
        {
        }
    }
}