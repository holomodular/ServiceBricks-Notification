namespace ServiceBricks.Notification.Sqlite
{
    /// <summary>
    /// These are constants for the ServiceBricks Notification Sqlite module.
    /// </summary>
    public static partial class NotificationSqliteConstants
    {
        /// <summary>
        /// AppSetting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:Sqlite:ConnectionString";

        /// <summary>
        /// The default database schema name.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Notification";
    }
}