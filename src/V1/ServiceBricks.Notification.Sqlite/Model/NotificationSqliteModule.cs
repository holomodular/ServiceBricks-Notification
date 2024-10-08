﻿using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Sqlite
{
    /// <summary>
    /// The module definition for the ServiceBricks Notification Sqlite module.
    /// </summary>
    public partial class NotificationSqliteModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance.
        /// </summary>
        public static NotificationSqliteModule Instance = new NotificationSqliteModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationSqliteModule()
        {
            DependentModules = new List<IModule>()
            {
                new NotificationEntityFrameworkCoreModule()
            };
        }
    }
}