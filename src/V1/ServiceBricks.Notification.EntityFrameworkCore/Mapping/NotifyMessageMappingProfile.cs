namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// Mapping profile for the NotifyMessage domain object.
    /// </summary>
    public partial class NotifyMessageMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<NotifyMessage, NotifyMessageDto>(
                (s, d) =>
                {
                    d.BccAddress = s.BccAddress;
                    d.Body = s.Body;
                    d.BodyHtml = s.BodyHtml;
                    d.CcAddress = s.CcAddress;
                    d.CreateDate = s.CreateDate;
                    d.FromAddress = s.FromAddress;
                    d.FutureProcessDate = s.FutureProcessDate;
                    d.IsComplete = s.IsComplete;
                    d.IsError = s.IsComplete;
                    d.IsHtml = s.IsHtml;
                    d.IsProcessing = s.IsProcessing;
                    d.Priority = s.Priority;
                    d.ProcessDate = s.ProcessDate;
                    d.ProcessResponse = s.ProcessResponse;
                    d.RetryCount = s.RetryCount;
                    d.SenderType = s.SenderType;
                    d.StorageKey = s.Key.ToString();
                    d.Subject = s.Subject;
                    d.ToAddress = s.ToAddress;
                    d.UpdateDate = s.UpdateDate;
                });

            registry.Register<NotifyMessageDto, NotifyMessage>(
                (s, d) =>
                {
                    d.BccAddress = s.BccAddress;
                    d.Body = s.Body;
                    d.BodyHtml = s.BodyHtml;
                    d.CcAddress = s.CcAddress;
                    //d.CreateDate ignore
                    d.FromAddress = s.FromAddress;
                    d.FutureProcessDate = s.FutureProcessDate;
                    d.IsComplete = s.IsComplete;
                    d.IsError = s.IsComplete;
                    d.IsHtml = s.IsHtml;
                    d.IsProcessing = s.IsProcessing;                    
                    if (long.TryParse(s.StorageKey, out var tempKey))
                        d.Key = tempKey;
                    d.Priority = s.Priority;
                    d.ProcessDate = s.ProcessDate;
                    d.ProcessResponse = s.ProcessResponse;
                    d.RetryCount = s.RetryCount;
                    d.SenderType = s.SenderType;
                    d.Subject = s.Subject;
                    d.ToAddress = s.ToAddress;
                    d.UpdateDate = s.UpdateDate;
                });
        }
    }
}