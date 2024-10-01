namespace ServiceBricks.Notification.SendGrid
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification SendGrid module.
    /// </summary>
    public partial class NotificationSendGridModule : ServiceBricks.Module
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationSendGridModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }
    }
}