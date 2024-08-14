namespace ServiceBricks.Notification
{
    /// <summary>
    /// Options for the SMTP provider.
    /// </summary>
    public partial class SmtpOptions
    {
        /// <summary>
        /// Email setting for host name.
        /// </summary>
        public string EmailServer { get; set; }

        /// <summary>
        /// Port.
        /// </summary>
        public int EmailPort { get; set; }

        /// <summary>
        /// Determine if request should use SSL.
        /// </summary>
        public bool EmailEnableSsl { get; set; }

        /// <summary>
        /// The username for authentication.
        /// </summary>
        public string EmailUsername { get; set; }

        /// <summary>
        /// The password for authentication.
        /// </summary>
        public string EmailPassword { get; set; }
    }
}