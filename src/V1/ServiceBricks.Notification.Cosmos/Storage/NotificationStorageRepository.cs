﻿using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is the storage repository for the Notification module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class NotificationStorageRepository<TDomain> : EntityFrameworkCoreStorageRepository<TDomain>, IRepository<TDomain>
        where TDomain : class, IEntityFrameworkCoreDomainObject<TDomain>, new()
    {
        public NotificationStorageRepository(ILoggerFactory logFactory, NotificationCosmosContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}