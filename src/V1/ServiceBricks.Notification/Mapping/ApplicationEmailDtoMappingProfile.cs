using static System.Net.Mime.MediaTypeNames;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a mapping profile for the ApplicationEmailDto.
    /// </summary>
    public partial class ApplicationEmailDtoMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<ApplicationEmailDto, NotifyMessageDto>(
                (s, d) =>
                {
                    d.BccAddress = s.BccAddress;
                    d.Body = s.Body;
                    d.BodyHtml = s.BodyHtml;
                    d.CcAddress = s.CcAddress;
                    //d.CreateDate ignore
                    d.FromAddress = s.FromAddress;
                    d.FutureProcessDate = s.FutureProcessDate.HasValue ? s.FutureProcessDate.Value : DateTimeOffset.UtcNow;
                    //d.IsComplete ignore
                    //d.IsError ignore
                    d.IsHtml = s.IsHtml;
                    //d.IsProcessing ignore
                    d.Priority = s.Priority;
                    //d.ProcessDate ignore
                    //d.ProcessResponse ignore
                    //d.RetryCount ignore
                    d.SenderType = SenderType.Email_TEXT;
                    d.StorageKey = s.StorageKey;
                    d.Subject = s.Subject;
                    d.ToAddress = s.ToAddress;
                    //d.UpdateDate ignore
                });
        }
    }
}