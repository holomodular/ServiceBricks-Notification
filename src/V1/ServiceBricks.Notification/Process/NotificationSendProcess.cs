namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a process that sends a notification.
    /// </summary>
    public partial class NotificationSendProcess : DomainProcess<NotifyMessageDto>
    {
        public NotificationSendProcess(NotifyMessageDto message)
        {
            DomainObject = message;
        }
    }
}