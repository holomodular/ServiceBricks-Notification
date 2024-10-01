using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Postgres
{
    /// <summary>
    /// The module definition for the Notification Postgres module.
    /// </summary>
    public partial class NotificationPostgresModule : ServiceBricks.Module
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationPostgresModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationEntityFrameworkCoreModule()
            };
        }
    }
}