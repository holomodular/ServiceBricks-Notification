namespace ServiceBricks.Notification
{
    /// <summary>
    /// Options class for Notification configurations.
    /// </summary>
    public partial class SendOptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendOptions()
        {
            TimerIntervalMilliseconds = 7000;
            TimerDueMilliseconds = 1000;
        }

        /// <summary>
        /// Determine if the timer is enabled
        /// </summary>
        public virtual bool TimerEnabled { get; set; }

        /// <summary>
        /// The timer interval.
        /// </summary>
        public virtual int TimerIntervalMilliseconds { get; set; }

        /// <summary>
        /// The timer due time (1st run).
        /// </summary>
        public virtual int TimerDueMilliseconds { get; set; }

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