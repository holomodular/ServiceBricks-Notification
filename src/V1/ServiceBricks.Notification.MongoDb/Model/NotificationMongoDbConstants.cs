namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// These are constants for the Log module.
    /// </summary>
    public static partial class NotificationMongoDbConstants
    {
        /// <summary>
        /// AppSetting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:MongoDb:ConnectionString";

        /// <summary>
        /// AppSetting key for the database name.
        /// </summary>
        public const string APPSETTING_DATABASE = "ServiceBricks:Notification:Storage:MongoDb:Database";

        /// <summary>
        /// The prefix for the collection name.
        /// </summary>
        public const string COLLECTIONNAME_PREFIX = "Notification";

        /// <summary>
        /// Get the collection name for the table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}