namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a notification message data transfer object.
    /// </summary>
    public partial class NotifyMessageDto : DataTransferObject
    {
        /// <summary>
        /// The type of notification message.
        /// </summary>
        public virtual string SenderType { get; set; }

        /// <summary>
        /// Determine if processing encountered an error.
        /// </summary>
        public virtual bool IsError { get; set; }

        /// <summary>
        /// Determine if processing has completed.
        /// </summary>
        public virtual bool IsComplete { get; set; }

        /// <summary>
        /// The current number of retries.
        /// </summary>
        public virtual int RetryCount { get; set; }

        /// <summary>
        /// The future process date.
        /// </summary>
        public virtual DateTimeOffset FutureProcessDate { get; set; }

        /// <summary>
        /// The date and time the message was created.
        /// </summary>
        public virtual DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The date and time the message was last updated.
        /// </summary>
        public virtual DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// The date and time the message was processed.
        /// </summary>
        public virtual DateTimeOffset ProcessDate { get; set; }

        /// <summary>
        /// The processing response.
        /// </summary>
        public virtual string ProcessResponse { get; set; }

        /// <summary>
        /// Determine if the message is currently processing.
        /// </summary>
        public virtual bool IsProcessing { get; set; }

        /// <summary>
        /// Determine if the message is in html.
        /// </summary>
        public virtual bool IsHtml { get; set; }

        /// <summary>
        /// The priority of the message.
        /// </summary>
        public virtual string Priority { get; set; }

        /// <summary>
        /// The subject of the message.
        /// </summary>
        public virtual string Subject { get; set; }

        /// <summary>
        /// The BCC address of the message.
        /// </summary>
        public virtual string BccAddress { get; set; }

        /// <summary>
        /// The CC address of the message.
        /// </summary>
        public virtual string CcAddress { get; set; }

        /// <summary>
        /// The to address of the message.
        /// </summary>
        public virtual string ToAddress { get; set; }

        /// <summary>
        /// The from address of the message.
        /// </summary>
        public virtual string FromAddress { get; set; }

        /// <summary>
        /// The body of the message.
        /// </summary>
        public virtual string Body { get; set; }

        /// <summary>
        /// The html body of the message.
        /// </summary>
        public virtual string BodyHtml { get; set; }
    }
}