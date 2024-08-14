using System.Reflection;

namespace ServiceBricks.Notification.SendGrid
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification SendGrid module.
    /// </summary>
    public partial class NotificationSendGridModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationSendGridModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }

        /// <summary>
        /// The list of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of assemblies that contain the AutoMapper profiles for the module.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of assemblies that contain the view models for the module.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}