using System.Reflection;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// The module definition for the ServiceBricks.Notification.EntityFrameworkCore module.
    /// </summary>
    public partial class NotificationEntityFrameworkCoreModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance.
        /// </summary>
        public static NotificationEntityFrameworkCoreModule Instance = new NotificationEntityFrameworkCoreModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationEntityFrameworkCoreModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }
    }
}