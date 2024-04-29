using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.SqlServer
{
    public class NotificationSqlServerModule : IModule
    {
        public NotificationSqlServerModule()
        {
            AdminHtml = string.Empty;
            Name = "Notification EntityFrameworkCore Brick";
            Description = @"The Notification EntityFrameworkCore Brick implements the EntityFrameworkCore provider.";
            DependentModules = new List<IModule>()
            {
                new NotificationEntityFrameworkCoreModule()
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