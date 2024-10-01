using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.SqlServer
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification SqlServer module.
    /// </summary>
    public partial class NotificationSqlServerModule : ServiceBricks.Module
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationSqlServerModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationEntityFrameworkCoreModule()
            };
        }
    }
}