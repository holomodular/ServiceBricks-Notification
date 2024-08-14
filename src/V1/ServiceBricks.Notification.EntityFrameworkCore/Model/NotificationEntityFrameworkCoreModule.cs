using System.Reflection;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// The module definition for the ServiceBricks.Notification.EntityFrameworkCore module.
    /// </summary>
    public partial class NotificationEntityFrameworkCoreModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationEntityFrameworkCoreModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationEntityFrameworkCoreModule).Assembly
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
        /// The list of Automapper assemblies.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of view assemblies.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}