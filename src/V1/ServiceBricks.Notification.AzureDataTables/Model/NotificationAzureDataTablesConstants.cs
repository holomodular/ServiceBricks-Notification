namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class NotificationAzureDataTablesConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:AzureDataTables:ConnectionString";

        public const string TABLENAME_PREFIX = "Notification";

        public static string GetTableName(string tableName)
        {
            return TABLENAME_PREFIX + tableName;
        }
    }
}