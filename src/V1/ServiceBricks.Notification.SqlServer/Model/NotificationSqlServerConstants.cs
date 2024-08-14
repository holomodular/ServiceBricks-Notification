namespace ServiceBricks.Notification.SqlServer
{
    /// <summary>
    /// These are constants for the ServiceBricks Notification SqlServer module.
    /// </summary>
    public static partial class NotificationSqlServerConstants
    {
        /// <summary>
        /// AppSetting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:SqlServer:ConnectionString";

        /// <summary>
        /// The default database schema name.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Notification";
    }
}