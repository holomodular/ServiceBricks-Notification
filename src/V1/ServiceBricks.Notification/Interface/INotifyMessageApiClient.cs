namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an API service for the notification message domain object.
    /// </summary>
    public interface INotifyMessageApiClient : IApiClient<NotifyMessageDto>, INotifyMessageApiService
    {
    }
}