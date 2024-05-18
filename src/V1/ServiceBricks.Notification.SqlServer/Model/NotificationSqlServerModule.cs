using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.SqlServer
{
    public class NotificationSqlServerModule : IModule
    {
        public NotificationSqlServerModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationEntityFrameworkCoreModule()
            };
        }

        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
        public List<IModule> DependentModules { get; }
    }
}