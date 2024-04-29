namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is constants for the Notification module.
    /// </summary>
    public static class NotificationCosmosConstants
    {
        public const string APPSETTING_CONNECTION = "ServiceBricks:Notification:Cosmos:ConnectionString";
        public const string APPSETTING_DATABASE = "ServiceBricks:Notification:Cosmos:Database";

        public const string DEFAULT_CONTAINER_NAME = "Notification";
    }
}