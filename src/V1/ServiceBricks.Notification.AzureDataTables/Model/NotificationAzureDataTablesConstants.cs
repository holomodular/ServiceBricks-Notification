namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// These are constants for the ServiceBricks.Notification.AzureDataTables module.
    /// </summary>
    public static partial class NotificationAzureDataTablesConstants
    {
        /// <summary>
        /// AppSetting key for the Azure Data Tables connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:AzureDataTables:ConnectionString";

        /// <summary>
        /// AppSetting key for the Azure Data Tables table name prefix.
        /// </summary>
        public const string TABLENAME_PREFIX = "Notification";

        /// <summary>
        /// Get the table name with the prefix.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableName(string tableName)
        {
            return TABLENAME_PREFIX + tableName;
        }
    }
}