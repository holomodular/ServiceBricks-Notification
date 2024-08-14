using System.Reflection;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// The module definition for the Notification MongoDb module.
    /// </summary>
    public partial class NotificationMongoDbModule : IModule
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

        /// <summary>
        /// The list of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of assemblies that contain the Automapper mappings.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of assemblies that contain views.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}