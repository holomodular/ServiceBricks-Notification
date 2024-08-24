namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// These are constants for the ServiceBricks Notification Cosmos module.
    /// </summary>
    public static partial class NotificationCosmosConstants
    {
        /// <summary>
        /// AppSetting key for the Cosmos connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:Cosmos:ConnectionString";

        /// <summary>
        /// AppSetting key for the Cosmos database.
        /// </summary>
        public const string APPSETTING_DATABASE = "ServiceBricks:Notification:Storage:Cosmos:Database";

        /// <summary>
        /// Default container name for the Cosmos database.
        /// </summary>
        public const string CONTAINER_PREFIX = "Notification";

        /// <summary>
        /// Get the container name for the given table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetContainerName(string tableName)
        {
            return CONTAINER_PREFIX + tableName;
        }
    }
}