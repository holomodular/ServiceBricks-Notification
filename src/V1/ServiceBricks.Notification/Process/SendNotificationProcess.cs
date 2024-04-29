namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a process that sends a notification.
    /// </summary>
    public class SendNotificationProcess : DomainProcess<NotifyMessageDto>
    {
        public SendNotificationProcess()
        {
        }

        public SendNotificationProcess(NotifyMessageDto message)
        {
            DomainObject = message;
        }
    }
}