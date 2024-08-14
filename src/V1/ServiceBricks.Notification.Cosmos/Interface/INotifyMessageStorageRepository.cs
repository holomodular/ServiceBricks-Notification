namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is a storage repository for the NotifyMessage domain object.
    /// </summary>
    public partial interface INotifyMessageStorageRepository : IStorageRepository<NotifyMessage>, IDomainObjectProcessQueueStorageRepository<NotifyMessage>
    {
    }
}