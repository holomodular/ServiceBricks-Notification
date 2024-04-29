using AutoMapper;

using ServiceQuery;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is an API service for the message domain object.
    /// </summary>
    public class NotifyMessageApiService : ApiService<NotifyMessage, NotifyMessageDto>, INotifyMessageApiService
    {
        public NotifyMessageApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<NotifyMessage> repository)
            : base(mapper, businessRuleService, repository)
        {
        }

        public async Task<IResponseList<DomainTypeDto>> GetSenderTypesAsync()
        {
            var list = _mapper.Map<List<SenderType>, List<DomainTypeDto>>(SenderType.GetAll());
            return await Task.FromResult<IResponseList<DomainTypeDto>>(
                new ResponseList<DomainTypeDto>(list));
        }
    }
}