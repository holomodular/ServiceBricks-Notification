namespace ServiceBricks.Notification
{
    /// <summary>
    /// Notification options.
    /// </summary>
    public partial class NotificationOptions
    {
        /// <summary>
        /// If development environment, if so, remove all senders and use DevelopmentEmailTo or DevelopmentSmsTo
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
        public virtual string EmailFromDefault { get; set; }

        /// <summary>
        /// The default from address.
        /// </summary>
        public virtual string SmsFromDefault { get; set; }
    }
}