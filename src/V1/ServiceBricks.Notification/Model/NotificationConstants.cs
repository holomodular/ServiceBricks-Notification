namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is constants for the Notification module.
    /// </summary>
    public static class NotificationConstants
    {
        public const string APPSETTINGS_NOTIFICATION_OPTIONS = "ServiceBricks:Notification:Options";
        public const string APPSETTINGS_SMTP_PROVIDER_OPTIONS = "ServiceBricks:Notification:Smtp";
        public const string APPSETTING_CLIENT_APICONFIG = @"ServiceBricks:Notification:Client:ApiConfig";

        public const string MFA_PROVIDER_PHONE = "Phone";
        public const string MFA_PROVIDER_EMAIL = "Email";
    }
}