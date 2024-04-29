using AutoMapper;

using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Notification.MongoDb
{
    public class NotifyMessageMappingProfile : Profile
    {
        public NotifyMessageMappingProfile()
        {
            CreateMap<NotifyMessageDto, NotifyMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom(z => z.StorageKey));

            CreateMap<NotifyMessage, NotifyMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }
    }
}