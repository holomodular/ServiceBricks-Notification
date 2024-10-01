using Microsoft.Extensions.Logging;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Postgres
{
    /// <summary>
    /// This is a storage repository for the notification message domain object.
    /// </summary>
    public partial class NotifyMessageStorageRepository : NotificationStorageRepository<NotifyMessage>, INotifyMessageStorageRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="context"></param>
        public NotifyMessageStorageRepository(
            ILoggerFactory loggerFactory,
            NotificationPostgresContext context) : base(loggerFactory, context)
        { }

        /// <summary>
        /// Get the queue items.
        /// </summary>
        /// <param name="batchNumberToTake"></param>
        /// <param name="pickupErrors"></param>
        /// <param name="errorPickupCutoffDate"></param>
        /// <returns></returns>
        public async Task<IResponseList<NotifyMessage>> GetQueueItemsAsync(int batchNumberToTake, bool pickupErrors, DateTimeOffset errorPickupCutoffDate)
        {
            ResponseList<NotifyMessage> response = new ResponseList<NotifyMessage>();
            try
            {
                var query = DomainProcessQueueService<NotifyMessage>.GetQueueItemsQuery(
                    batchNumberToTake,
                    pickupErrors,
                    errorPickupCutoffDate);

                DateTimeOffset now = DateTimeOffset.UtcNow;

                var respQuery = await this.QueryAsync(query);
                response.CopyFrom(respQuery);
                if (response.Error)
                    return response;

                if (respQuery.Item.List != null && respQuery.Item.List.Count > 0)
                {
                    foreach (var item in respQuery.Item.List)
                    {
                        item.IsProcessing = true;
                        item.ProcessDate = now;
                        item.UpdateDate = now;
                        await UpdateAsync(item);
                    }
                    response.List = respQuery.Item.List;
                }
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