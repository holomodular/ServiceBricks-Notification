using AutoMapper;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a mapping profile for the ApplicationSmsDto.
    /// </summary>
    public partial class ApplicationSmsDtoMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ApplicationSmsDtoMappingProfile() : base()
        {
            // AI: Create a automapper mapping for the ApplicationSmsDto and NotifyMessageDto.
            CreateMap<ApplicationSmsDto, NotifyMessageDto>()
                .ForMember(x => x.BccAddress, y => y.Ignore())
                .ForMember(x => x.Body, y => y.Ignore())
                .ForMember(x => x.BodyHtml, y => y.Ignore())
                .ForMember(x => x.CcAddress, y => y.Ignore())
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.FromAddress, y => y.Ignore())
                .ForMember(x => x.FutureProcessDate, y => y.MapFrom(z => z.FutureProcessDate))
                .ForMember(x => x.IsComplete, y => y.Ignore())
                .ForMember(x => x.IsError, y => y.Ignore())
                .ForMember(x => x.IsHtml, y => y.Ignore())
                .ForMember(x => x.IsProcessing, y => y.Ignore())
                .ForMember(x => x.Priority, y => y.Ignore())
                .ForMember(x => x.ProcessDate, y => y.Ignore())
                .ForMember(x => x.ProcessResponse, y => y.Ignore())
                .ForMember(x => x.RetryCount, y => y.Ignore())
                .ForMember(x => x.SenderType, y => y.MapFrom(z => SenderType.SMS_TEXT))
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.StorageKey))
                .ForMember(x => x.Subject, y => y.Ignore())
                .ForMember(x => x.ToAddress, y => y.MapFrom(z => z.PhoneNumber))
                .ForMember(x => x.UpdateDate, y => y.Ignore());
        }
    }
}