using System.Reflection;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// The module definition for the Notification module.
    /// </summary>
    public partial class NotificationModule : IModule
    {
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