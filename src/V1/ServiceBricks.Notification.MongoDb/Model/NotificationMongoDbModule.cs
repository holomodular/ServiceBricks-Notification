using System.Reflection;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// The module definition for the Notification MongoDb module.
    /// </summary>
    public partial class NotificationMongoDbModule : ServiceBricks.Module
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationMongoDbModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationMongoDbModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }
    }
}