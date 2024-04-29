namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is a storage repository for the notification message domain object.
    /// </summary>
    public interface INotifyMessageStorageRepository : IStorageRepository<NotifyMessage>, IDomainObjectProcessQueueStorageRepository<NotifyMessage>
    {
    }
}