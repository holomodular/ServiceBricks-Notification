namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class NotificationAzureDataTablesConstants
    {
        public const string APPSETTINGS_CONNECTION_STRING = "ServiceBricks:Notification:AzureDataTables:ConnectionString";

        public const string TABLENAME_PREFIX = "Notification";

        public static string GetTableName(string tableName)
        {
            return TABLENAME_PREFIX + tableName;
        }
    }
}