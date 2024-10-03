using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.InMemory
{
    /// <summary>
    /// The module definition for the in-memory notification module.
    /// </summary>
    public partial class NotificationInMemoryModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static NotificationInMemoryModule Instance = new NotificationInMemoryModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationInMemoryModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationEntityFrameworkCoreModule()
            };
        }
    }
}