using AutoMapper;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a mapping profile for the SenderType.
    /// </summary>
    public partial class SenderTypeMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SenderTypeMappingProfile() : base()
        {
            // AI: Create a automapper mapping for the SenderType and DomainTypeDto.
            CreateMap<SenderType, DomainTypeDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));

            CreateMap<DomainTypeDto, SenderType>()
                .ForMember(x => x.Key, y => y.MapFrom(z => z.StorageKey))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}