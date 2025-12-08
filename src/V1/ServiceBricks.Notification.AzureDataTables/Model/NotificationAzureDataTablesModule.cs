using System.Reflection;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// The module definition for the NotificationAzureDataTables module.
    /// </summary>
    public partial class NotificationAzureDataTablesModule : ServiceBricks.Module
    {
        public static NotificationAzureDataTablesModule Instance = new NotificationAzureDataTablesModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationAzureDataTablesModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationModule()
            };
        }
    }
}