using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.Postgres
{
    /// <summary>
    /// The module definition for the Notification Postgres module.
    /// </summary>
    public partial class NotificationPostgresModule : IModule
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