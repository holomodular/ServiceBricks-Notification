namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a process that sends a notification.
    /// </summary>
    public partial class SendNotificationProcess : DomainProcess<NotifyMessageDto>
    {
        public SendNotificationProcess(NotifyMessageDto message)
        {
            DomainObject = message;
        }
    }
}