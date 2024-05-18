using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceQuery;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is a storage repository for the notification message domain object.
    /// </summary>
    public class NotifyMessageStorageRepository : NotificationStorageRepository<NotifyMessage>, INotifyMessageStorageRepository
    {
        public NotifyMessageStorageRepository(
            ILoggerFactory loggerFactory,
            NotificationCosmosContext context) : base(loggerFactory, context)
        { }

        public async Task<IResponseList<NotifyMessage>> GetQueueItemsAsync(int batchNumberToTake, bool pickupErrors, DateTimeOffset errorPickupCutoffDate)
        {
            ResponseList<NotifyMessage> response = new ResponseList<NotifyMessage>();
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;

                ServiceQueryRequestBuilder qb = new ServiceQueryRequestBuilder();
                qb.IsEqual(nameof(NotifyMessage.IsComplete), false.ToString())
                    .And()
                    .IsEqual(nameof(NotifyMessage.IsProcessing), false.ToString())
                    .And()
                    .IsLessThanOrEqual(nameof(NotifyMessage.FutureProcessDate), now.ToString("o"));
                if (pickupErrors)
                {
                    qb.And()
                    .IsEqual(nameof(NotifyMessage.IsError), true.ToString())
                    .And()
                    .IsLessThanOrEqual(nameof(NotifyMessage.ProcessDate), errorPickupCutoffDate.ToString("o"));
                }
                else
                {
                    qb.And()
                    .IsEqual(nameof(NotifyMessage.IsError), false.ToString());
                }
                qb.Sort(nameof(NotifyMessage.CreateDate), true);
                qb.Paging(1, batchNumberToTake, false);

                var respQuery = await this.QueryAsync(qb.Build());
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