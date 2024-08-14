using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.SqlServer
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification SqlServer module.
    /// </summary>
    public partial class NotificationSqlServerModule : IModule
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