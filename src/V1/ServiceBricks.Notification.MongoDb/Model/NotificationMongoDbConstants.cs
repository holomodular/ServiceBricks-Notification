namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class NotificationMongoDbConstants
    {
        public const string APPSETTINGS_CONNECTION_STRING = "ServiceBricks:Notification:MongoDb:ConnectionString";
        public const string APPSETTINGS_DATABASE_NAME = "ServiceBricks:Notification:MongoDb:DatabaseName";

        public const string COLLECTIONNAME_PREFIX = "Notification";

        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}