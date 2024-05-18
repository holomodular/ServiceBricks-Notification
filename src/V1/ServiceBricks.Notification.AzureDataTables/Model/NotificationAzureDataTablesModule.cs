using System.Reflection;

namespace ServiceBricks.Notification.AzureDataTables
{
    public class NotificationAzureDataTablesModule : IModule
    {
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

        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }

        public List<IModule> DependentModules { get; }
    }
}