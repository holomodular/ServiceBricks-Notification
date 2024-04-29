using System.Reflection;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    public class NotificationEntityFrameworkCoreModule : IModule
    {
        public NotificationEntityFrameworkCoreModule()
        {
            AdminHtml = string.Empty;
            Name = "Notification EntityFrameworkCore Brick";
            Description = @"The Notification EntityFrameworkCore Brick implements the EntityFrameworkCore provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationEntityFrameworkCoreModule).Assembly
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