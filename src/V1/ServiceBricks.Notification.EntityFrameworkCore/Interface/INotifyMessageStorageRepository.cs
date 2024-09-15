namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// This is a storage repository for the NotifyMessage domain object.
    /// </summary>
    public partial interface INotifyMessageStorageRepository : IStorageRepository<NotifyMessage>, IDomainProcessQueueStorageRepository<NotifyMessage>
    {
    }
}