using System.Reflection;

namespace ServiceBricks.Notification.SendGrid
{
    public class NotificationSendGridModule : IModule
    {
        public NotificationSendGridModule()
        {
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