namespace ServiceBricks.Notification
{
    /// <summary>
    /// These are constants for the ServiceBricks.Notification module.
    /// </summary>
    public static partial class NotificationConstants
    {
        /// <summary>
        /// AppSettings key for the Notification options.
        /// </summary>
        public const string APPSETTINGS_SEND_OPTIONS = "ServiceBricks:Notification:Send";

        /// <summary>
        /// AppSettings key for the SMTP provider options.
        /// </summary>
        public const string APPSETTINGS_SMTP_PROVIDER_OPTIONS = "ServiceBricks:Notification:Smtp";

        /// <summary>
        /// AppSettings key for the client options.
        /// </summary>
        public const string APPSETTING_CLIENT_APICONFIG = @"ServiceBricks:Notification:Client:Api";

    }
}