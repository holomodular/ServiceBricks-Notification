using AutoMapper;

namespace ServiceBricks.Notification.EntityFrameworkCore
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
            // AI: Add mappings for the domain object
            CreateMap<NotifyMessageDto, NotifyMessage>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom<KeyResolver>());

            CreateMap<NotifyMessage, NotifyMessageDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }

        /// <summary>
        /// Resolve the key.
        /// </summary>
        public class KeyResolver : IValueResolver<DataTransferObject, object, long>
        {
            /// <summary>
            /// Resolve the key.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="sourceMember"></param>
            /// <param name="context"></param>
            /// <returns></returns>
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