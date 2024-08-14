using System.Reflection;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// The module definition for the NotificationAzureDataTables module.
    /// </summary>
    public partial class NotificationAzureDataTablesModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationAzureDataTablesModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationAzureDataTablesModule).Assembly
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