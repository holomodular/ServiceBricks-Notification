using System.Reflection;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification Cosmos module.
    /// </summary>
    public partial class NotificationCosmosModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationCosmosModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationCosmosModule).Assembly
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
        /// The list of assemblies that contain Automapper profiles.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of assemblies that contain views.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}