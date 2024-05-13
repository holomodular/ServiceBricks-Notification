using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is the storage repository for the Notification module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class NotificationStorageRepository<TDomain> : MongoDbStorageRepository<TDomain>
        where TDomain : class, IMongoDbDomainObject<TDomain>, new()
    {
        public NotificationStorageRepository(
            ILoggerFactory logFactory,
            IConfiguration configuration)
            : base(logFactory)
        {
            ConnectionString = configuration.GetMongoDbConnectionString(
                NotificationMongoDbConstants.APPSETTING_CONNECTION_STRING);
            DatabaseName = configuration.GetMongoDbDatabase(
                NotificationMongoDbConstants.APPSETTING_DATABASE);
            CollectionName = NotificationMongoDbConstants.GetCollectionName(typeof(TDomain).Name);
        }
    }
}