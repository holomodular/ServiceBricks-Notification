using System.Reflection;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    public class NotificationEntityFrameworkCoreModule : IModule
    {
        public NotificationEntityFrameworkCoreModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationEntityFrameworkCoreModule).Assembly
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