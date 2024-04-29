namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a notification message data transfer object.
    /// </summary>
    public class NotifyMessageDto : DataTransferObject
    {
        public int SenderTypeKey { get; set; }
        public bool IsError { get; set; }
        public bool IsComplete { get; set; }
        public int RetryCount { get; set; }
        public DateTimeOffset FutureProcessDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset ProcessDate { get; set; }
        public string ProcessResponse { get; set; }
        public bool IsProcessing { get; set; }

        public virtual bool IsHtml { get; set; }
        public virtual string Priority { get; set; }
        public virtual string Subject { get; set; }
        public virtual string BccAddress { get; set; }
        public virtual string CcAddress { get; set; }
        public virtual string ToAddress { get; set; }
        public virtual string FromAddress { get; set; }
        public virtual string Body { get; set; }
        public virtual string BodyHtml { get; set; }
    }
}