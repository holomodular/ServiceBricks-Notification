using AutoMapper;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    public class NotifyMessageMappingProfile : Profile
    {
        public NotifyMessageMappingProfile()
        {
            CreateMap<NotifyMessageDto, NotifyMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom<KeyResolver>());

            CreateMap<NotifyMessage, NotifyMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }

        public class KeyResolver : IValueResolver<DataTransferObject, object, long>
        {
            public long Resolve(DataTransferObject source, object destination, long sourceMember, ResolutionContext context)
            {
                if (string.IsNullOrEmpty(source.StorageKey))
                    return 0;

                long tempKey;
                if (long.TryParse(source.StorageKey, out tempKey))
                    return tempKey;
                return 0;
            }
        }
    }
}