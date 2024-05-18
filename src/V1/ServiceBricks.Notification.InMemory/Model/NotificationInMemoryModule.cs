using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.InMemory
{
    public class NotificationInMemoryModule : IModule
    {
        public NotificationInMemoryModule()
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