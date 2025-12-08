using System.Reflection;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// The module definition for the Notification MongoDb module.
    /// </summary>
    public partial class NotificationMongoDbModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance.
        /// </summary>
        public static NotificationMongoDbModule Instance = new NotificationMongoDbModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationMongoDbModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }
    }
}