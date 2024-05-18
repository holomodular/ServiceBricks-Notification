using System.Collections.Generic;
using System.Reflection;

namespace ServiceBricks.Notification
{
    public class NotificationModule : IModule
    {
        public NotificationModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationModule).Assembly
            };
        }

        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
        public List<IModule> DependentModules { get; }
    }
}