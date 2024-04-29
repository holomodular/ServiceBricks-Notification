using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is a notification message.
    /// </summary>
    public class NotifyMessage : MongoDbDomainObject<NotifyMessage>, IDpCreateDate, IDpUpdateDate, IDpProcessQueue
    {
        public NotifyMessage()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Key { get; set; }

        public virtual int SenderTypeKey { get; set; }
        public virtual bool IsError { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual int RetryCount { get; set; }
        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual DateTimeOffset UpdateDate { get; set; }
        public virtual DateTimeOffset FutureProcessDate { get; set; }
        public virtual DateTimeOffset ProcessDate { get; set; }
        public virtual string ProcessResponse { get; set; }
        public virtual bool IsProcessing { get; set; }

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