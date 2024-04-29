using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Postgres
{
    /// <summary>
    /// This is a storage repository for the notification message domain object.
    /// </summary>
    public class MessageStorageRepository : NotificationStorageRepository<NotifyMessage>, INotifyMessageStorageRepository
    {
        public MessageStorageRepository(
            ILoggerFactory loggerFactory,
            NotificationPostgresContext context) : base(loggerFactory, context)
        { }

        public async Task<IResponseList<NotifyMessage>> GetQueueItemsAsync(int batchNumberToTake, bool pickupErrors, DateTimeOffset errorPickupCutoffDate)
        {
            ResponseList<NotifyMessage> response = new ResponseList<NotifyMessage>();
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                string sql = $"UPDATE TOP({batchNumberToTake}) SET " +
                    $" [IsProcessing] = 1, [ProcessDate] = '{now}', UpdateDate='{now}'" +
                    $" FROM [{NotificationEntityFrameworkCoreConstants.DATABASE_SCHEMA_NAME}].[{nameof(NotifyMessage)}] " +
                    " WITH (UPDLOCK, READPAST) " +
                    $" WHERE [IsComplete] = 0 AND [IsProcessing] = 0 AND [FutureProcessDate] <= '{now}' ";
                if (pickupErrors)
                    sql += $" AND [IsError] = 1 AND [ProcessDate] <= '{errorPickupCutoffDate}' ";
                else
                    sql += " AND [IsError] = 0 ";
                sql += "ORDER BY [CreateDate] ASC " +
                    " OUTPUT INSERTED.*";
                var list = await DbSet.FromSqlRaw(sql).ToListAsync();
                await SaveChangesAsync();
                response.List = list;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_STORAGE));
            }
            return response;
        }
    }
}