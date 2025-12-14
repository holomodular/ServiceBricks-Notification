namespace ServiceBricks.Notification
{
    /// <summary>
    /// A domain type for the Sender.
    /// </summary>
    public partial class SenderType : DomainType
    {
        /// <summary>
        /// Email
        /// </summary>
        public const string Email_TEXT = "email";

        /// <summary>
        /// SMS
        /// </summary>
        public const string SMS_TEXT = "sms";

        /// <summary>
        /// Get all SenderTypes.
        /// </summary>
        /// <returns></returns>
        public static List<SenderType> GetAll()
        {
            return new List<SenderType>()
            {
                new SenderType(){ Key = Email_TEXT, Name = Email_TEXT },
                new SenderType(){ Key = SMS_TEXT, Name = SMS_TEXT },
            };
        }
    }
}