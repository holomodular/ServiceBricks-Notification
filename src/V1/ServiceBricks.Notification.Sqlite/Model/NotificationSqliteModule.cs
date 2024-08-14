using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.Sqlite
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification Sqlite module.
    /// </summary>
    public partial class NotificationSqliteModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationSqliteModule()
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
        /// The list of assemblies that contain the AutoMapper profiles for the module.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of assemblies that contain the views.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}