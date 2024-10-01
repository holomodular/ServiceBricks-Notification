using System.Reflection;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// The module definition for the Notification module.
    /// </summary>
    public partial class NotificationModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static NotificationModule Instance = new NotificationModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationModule).Assembly
            };
        }
    }
}