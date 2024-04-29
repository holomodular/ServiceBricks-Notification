using System.Reflection;

namespace ServiceBricks.Notification.MongoDb
{
    public class NotificationMongoDbModule : IModule
    {
        public NotificationMongoDbModule()
        {
            AdminHtml = string.Empty;
            Name = "Notification MongoDB Brick";
            Description = @"The Notification MongoDB Brick implements the MongoDB provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(NotificationMongoDbModule).Assembly
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