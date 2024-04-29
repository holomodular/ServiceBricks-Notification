using System.Reflection;

namespace ServiceBricks.Notification.AzureDataTables
{
    public class NotificationAzureDataTablesModule : IModule
    {
        public NotificationAzureDataTablesModule()
        {
            AdminHtml = string.Empty;
            Name = "Notification AzureDataTables Brick";
            Description = @"The Notification AzureDataTables Brick implements the AzureDataTables provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationAzureDataTablesModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }

        public string Name { get; }
        public string Description { get; }
        public string AdminHtml { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }

        public List<IModule> DependentModules { get; }
    }
}