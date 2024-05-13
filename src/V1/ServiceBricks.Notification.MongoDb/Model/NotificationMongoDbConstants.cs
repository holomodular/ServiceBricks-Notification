namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class NotificationMongoDbConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:MongoDb:ConnectionString";
        public const string APPSETTING_DATABASE = "ServiceBricks:Notification:Storage:MongoDb:Database";

        public const string COLLECTIONNAME_PREFIX = "Notification";

        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}