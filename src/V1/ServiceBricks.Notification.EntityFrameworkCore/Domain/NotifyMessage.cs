using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Notification.EntityFrameworkCore
{
    /// <summary>
    /// This is a notification message.
    /// </summary>
    public class NotifyMessage : EntityFrameworkCoreDomainObject<NotifyMessage>, IDpCreateDate, IDpUpdateDate, IDpProcessQueue
    {
        public NotifyMessage()
        {
        }

        public long Key { get; set; }
        public int SenderTypeKey { get; set; }
        public bool IsError { get; set; }
        public bool IsComplete { get; set; }
        public int RetryCount { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset FutureProcessDate { get; set; }
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

        public override Expression<Func<NotifyMessage, bool>> DomainGetItemFilter(NotifyMessage obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}