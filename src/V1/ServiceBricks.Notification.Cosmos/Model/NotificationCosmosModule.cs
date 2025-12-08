using System.Reflection;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification Cosmos module.
    /// </summary>
    public partial class NotificationCosmosModule : ServiceBricks.Module
    {
        public static NotificationCosmosModule Instance = new NotificationCosmosModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationCosmosModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }
    }
}