namespace ServiceBricks.Notification
{
    /// <summary>
    /// Options class for Notification configurations.
    /// </summary>
    public partial class NotificationOptions
    {
        /// <summary>
        /// If development environment, remove all senders from message and use DevelopmentEmailTo or DevelopmentSmsTo
        /// </summary>
        public bool IsDevelopment { get; set; }

        /// <summary>
        /// Used when IsDevelopement
        /// </summary>
        public string DevelopmentEmailTo { get; set; }

        /// <summary>
        /// Used when IsDevelopement
        /// </summary>
        public string DevelopmentSmsTo { get; set; }

        /// <summary>
        /// The default from adddress.
        /// </summary>
        public string EmailFromDefault { get; set; }

        /// <summary>
        /// The default from address.
        /// </summary>
        public string SmsFromDefault { get; set; }
    }
}