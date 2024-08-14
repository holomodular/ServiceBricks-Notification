using AutoMapper;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// Mapping profile for the NotifyMessage domain object.
    /// </summary>
    public partial class NotifyMessageMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotifyMessageMappingProfile()
        {
            // AI: Add mappings for the domain object to the DTO
            CreateMap<NotifyMessageDto, NotifyMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom(z => z.StorageKey));

            CreateMap<NotifyMessage, NotifyMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }
    }
}