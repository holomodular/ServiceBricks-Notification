namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is constants for the Notification module.
    /// </summary>
    public static class NotificationCosmosConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Notification:Storage:Cosmos:ConnectionString";
        public const string APPSETTING_DATABASE = "ServiceBricks:Notification:Storage:Cosmos:Database";

        public const string DEFAULT_CONTAINER_NAME = "Notification";
    }
}