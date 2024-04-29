namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// This is a storage repository for the notification message domain object.
    /// </summary>
    public interface INotifyMessageStorageRepository : IStorageRepository<NotifyMessage>, IDomainObjectProcessQueueStorageRepository<NotifyMessage>
    {
    }
}