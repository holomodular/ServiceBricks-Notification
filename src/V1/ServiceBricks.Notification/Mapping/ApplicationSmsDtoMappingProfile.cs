namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a mapping profile for the ApplicationSmsDto.
    /// </summary>
    public partial class ApplicationSmsDtoMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<ApplicationSmsDto, NotifyMessageDto>(
                (s, d) =>
                {
                    //d.BccAddress ignore
                    d.Body = s.Message;
                    //d.BodyHtml ignore
                    //d.CcAddress ignore
                    //d.CreateDate ignore
                    //d.FromAddress ignore
                    d.FutureProcessDate = s.FutureProcessDate.HasValue ? s.FutureProcessDate.Value : DateTimeOffset.UtcNow;
                    //d.IsComplete ignore
                    //d.IsError ignore
                    //d.IsHtml = ignore
                    //d.IsProcessing ignore
                    //d.Priority = ignore
                    //d.ProcessDate ignore
                    //d.ProcessResponse ignore
                    //d.RetryCount ignore
                    d.SenderType = SenderType.SMS_TEXT;
                    d.StorageKey = s.StorageKey;
                    d.Subject = s.Message;
                    d.ToAddress = s.PhoneNumber;
                    //d.UpdateDate ignore
                });
        }
    }
}