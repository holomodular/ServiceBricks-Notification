using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// This is an API service for the message domain object.
    /// </summary>
    public class NotifyMessageApiService : ApiService<NotifyMessage, NotifyMessageDto>, INotifyMessageApiService
    {
        public NotifyMessageApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<NotifyMessage> repository) : base(mapper, businessRuleService, repository)
        {
        }
    }
}