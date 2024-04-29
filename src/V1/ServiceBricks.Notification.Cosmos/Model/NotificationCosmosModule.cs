using ServiceBricks.Notification.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Notification.Cosmos
{
    public class NotificationCosmosModule : IModule
    {
        public NotificationCosmosModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationCosmosModule).Assembly
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