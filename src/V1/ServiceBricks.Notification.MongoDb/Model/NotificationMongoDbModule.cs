using System.Reflection;

namespace ServiceBricks.Notification.MongoDb
{
    public class NotificationMongoDbModule : IModule
    {
        public NotificationMongoDbModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationMongoDbModule).Assembly
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