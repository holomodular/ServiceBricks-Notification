using AutoMapper;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// Mapping profile for the NotifyMessage domain object.
    /// </summary>
    public partial class NotifyMessageMappingProfile : Profile
    {
        /// <summary>
        /// Constructor for the NotifyMessageMappingProfile.
        /// </summary>
        public NotifyMessageMappingProfile()
        {
            // AI: Add mappings for the domain object
            CreateMap<NotifyMessageDto, NotifyMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom<KeyResolver>());

            CreateMap<NotifyMessage, NotifyMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }

        /// <summary>
        /// Resolver for the Key property.
        /// </summary>
        public class KeyResolver : IValueResolver<DataTransferObject, object, Guid>
        {
            /// <summary>
            /// Resolve the Key property.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="sourceMember"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public Guid Resolve(DataTransferObject source, object destination, Guid sourceMember, ResolutionContext context)
            {
                if (string.IsNullOrEmpty(source.StorageKey))
                    return Guid.Empty;

                Guid tempKey;
                if (Guid.TryParse(source.StorageKey, out tempKey))
                    return tempKey;
                return Guid.Empty;
            }
        }
    }
}