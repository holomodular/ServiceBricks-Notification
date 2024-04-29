using System.Collections.Generic;
using System.Reflection;

namespace ServiceBricks.Notification
{
    public class NotificationModule : IModule
    {
        public NotificationModule()
        {
            AdminHtml = string.Empty;
            Name = "Notification Brick";
            Description = @"The Notification Brick is responsible for email and sms message delivery.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationModule).Assembly
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