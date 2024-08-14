using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is a notification message.
    /// </summary>
    public partial class NotifyMessage : EntityFrameworkCoreDomainObject<NotifyMessage>, IDpCreateDate, IDpUpdateDate, IDpProcessQueue
    {
        /// <summary>
        /// Internal primary key.
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// The type of notification message.
        /// </summary>
        public string SenderType { get; set; }

        /// <summary>
        /// Determine if processing encountered an error.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Determine if processing has completed.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// The current number of retries.
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// The future process date.
        /// </summary>
        public DateTimeOffset FutureProcessDate { get; set; }

        /// <summary>
        /// The date and time the message was created.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The date and time the message was last updated.
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// The date and time the message was processed.
        /// </summary>
        public DateTimeOffset ProcessDate { get; set; }

        /// <summary>
        /// The processing response.
        /// </summary>
        public string ProcessResponse { get; set; }

        /// <summary>
        /// Determine if the message is currently processing.
        /// </summary>
        public bool IsProcessing { get; set; }

        /// <summary>
        /// Determine if the message is in html.
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// The priority of the message.
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// The subject of the message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The BCC address of the message.
        /// </summary>
        public string BccAddress { get; set; }

        /// <summary>
        /// The CC address of the message.
        /// </summary>
        public string CcAddress { get; set; }

        /// <summary>
        /// The to address of the message.
        /// </summary>
        public string ToAddress { get; set; }

        /// <summary>
        /// The from address of the message.
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// The body of the message.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The html body of the message.
        /// </summary>
        public string BodyHtml { get; set; }

        /// <summary>
        /// Provide any defaults for the IQueryable object.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IQueryable<NotifyMessage> DomainGetIQueryableDefaults(IQueryable<NotifyMessage> query)
        {
            return query.OrderByDescending(x => x.CreateDate);
        }

        /// <summary>
        /// Provide an expression that will filter an object based on its primary key.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Expression<Func<NotifyMessage, bool>> DomainGetItemFilter(NotifyMessage obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}