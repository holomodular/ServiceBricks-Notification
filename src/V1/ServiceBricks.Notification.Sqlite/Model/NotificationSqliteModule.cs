using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.Sqlite
{
    public class NotificationSqliteModule : IModule
    {
        public NotificationSqliteModule()
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