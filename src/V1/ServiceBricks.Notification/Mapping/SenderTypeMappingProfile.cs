using AutoMapper;


namespace ServiceBricks.Notification
{
    public class SenderTypeMappingProfile : Profile
    {
        public SenderTypeMappingProfile() : base()
        {
            CreateMap<SenderType, DomainTypeDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));

            CreateMap<DomainTypeDto, SenderType>()
                .ForMember(x => x.Key, y => y.MapFrom(z => z.StorageKey))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}