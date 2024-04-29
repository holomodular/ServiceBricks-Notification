using System.Reflection;

namespace ServiceBricks.Notification.SendGrid
{
    public class NotificationSendGridModule : IModule
    {
        public NotificationSendGridModule()
        {
            AdminHtml = string.Empty;
            Name = "Notification SendGrid Brick";
            Description = @"The Notification SendGrid Brick implements the SendGrid provider.";
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