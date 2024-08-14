namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a REST API service for the NotifyMessageDto.
    /// </summary>
    public partial interface INotifyMessageApiService : IApiService<NotifyMessageDto>
    {
        /// <summary>
        /// Get a list of the sender types.
        /// </summary>
        /// <returns></returns>
        //Task<IResponseList<DomainTypeDto>> GetSenderTypesAsync();
    }
}