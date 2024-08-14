namespace ServiceBricks.Notification.Postgres
{
    /// <summary>
    /// These are constants for the ServiceBricks.Notification.Postgres module.
    /// </summary>
    public static partial class NotificationPostgresConstants
    {
        /// <summary>
        /// AppSetting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:Postgres:ConnectionString";

        /// <summary>
        /// The default database schema name.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Notification";
    }
}