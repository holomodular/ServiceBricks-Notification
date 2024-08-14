using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks.Notification.Cosmos module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class NotificationStorageRepository<TDomain> : EntityFrameworkCoreStorageRepository<TDomain>, IRepository<TDomain>
        where TDomain : class, IEntityFrameworkCoreDomainObject<TDomain>, new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="context"></param>
        public NotificationStorageRepository(
            ILoggerFactory logFactory,
            NotificationCosmosContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}