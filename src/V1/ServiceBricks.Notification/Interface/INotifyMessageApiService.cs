namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an API service for the notification message domain object.
    /// </summary>
    public interface INotifyMessageApiService : IApiService<NotifyMessageDto>
    {
        /// <summary>
        /// Get a list of the sender types.
        /// </summary>
        /// <returns></returns>
        //Task<IResponseList<DomainTypeDto>> GetSenderTypesAsync();
    }
}