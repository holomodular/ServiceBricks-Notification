using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This is the storage repository for the Notification module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class NotificationStorageRepository<TDomain> : AzureDataTablesStorageRepository<TDomain>
        where TDomain : class, IAzureDataTablesDomainObject<TDomain>, new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="configuration"></param>
        public NotificationStorageRepository(
            ILoggerFactory logFactory,
            IConfiguration configuration)
            : base(logFactory)
        {
            ConnectionString = configuration.GetAzureDataTablesConnectionString(
                NotificationAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);
            TableName = NotificationAzureDataTablesConstants.GetTableName(typeof(TDomain).Name);
        }
    }
}