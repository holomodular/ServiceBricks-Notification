using AutoMapper;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a mapping profile for the ApplicationEmailDto.
    /// </summary>
    public partial class ApplicationEmailDtoMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ApplicationEmailDtoMappingProfile() : base()
        {
            // AI: Create a automapper mapping for the ApplicationEmailDto and NotifyMessageDto.
            CreateMap<ApplicationEmailDto, NotifyMessageDto>()
                .ForMember(x => x.BccAddress, y => y.MapFrom(z => z.BccAddress))
                .ForMember(x => x.Body, y => y.MapFrom(z => z.Body))
                .ForMember(x => x.BodyHtml, y => y.MapFrom(z => z.BodyHtml))
                .ForMember(x => x.CcAddress, y => y.MapFrom(z => z.CcAddress))
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.FromAddress, y => y.MapFrom(z => z.FromAddress))
                .ForMember(x => x.FutureProcessDate, y => y.MapFrom(z => z.FutureProcessDate))
                .ForMember(x => x.IsComplete, y => y.Ignore())
                .ForMember(x => x.IsError, y => y.Ignore())
                .ForMember(x => x.IsHtml, y => y.MapFrom(z => z.IsHtml))
                .ForMember(x => x.IsProcessing, y => y.Ignore())
                .ForMember(x => x.Priority, y => y.MapFrom(z => z.Priority))
                .ForMember(x => x.ProcessDate, y => y.Ignore())
                .ForMember(x => x.ProcessResponse, y => y.Ignore())
                .ForMember(x => x.RetryCount, y => y.Ignore())
                .ForMember(x => x.SenderType, y => y.MapFrom(z => SenderType.Email_TEXT))
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.StorageKey))
                .ForMember(x => x.Subject, y => y.MapFrom(z => z.Subject))
                .ForMember(x => x.ToAddress, y => y.MapFrom(z => z.ToAddress))
                .ForMember(x => x.UpdateDate, y => y.Ignore());
        }
    }
}