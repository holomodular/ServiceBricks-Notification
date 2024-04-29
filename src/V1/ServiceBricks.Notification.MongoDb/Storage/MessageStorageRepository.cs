using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceQuery;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This is a storage repository for the notification message domain object.
    /// </summary>
    public class MessageStorageRepository : NotificationStorageRepository<NotifyMessage>, INotifyMessageStorageRepository
    {
        public MessageStorageRepository(
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
            : base(loggerFactory, configuration)
        { }

        public async Task<IResponseList<NotifyMessage>> GetQueueItemsAsync(int batchNumberToTake, bool pickupErrors, DateTimeOffset errorPickupCutoffDate)
        {
            ResponseList<NotifyMessage> response = new ResponseList<NotifyMessage>();
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;

                ServiceQueryRequestBuilder qb = new ServiceQueryRequestBuilder();
                qb.IsEqual(nameof(NotifyMessage.IsComplete), false.ToString())
                    .IsEqual(nameof(NotifyMessage.IsProcessing), false.ToString())
                    .IsLessThanOrEqual(nameof(NotifyMessage.FutureProcessDate), now.ToString());
                if (pickupErrors)
                {
                    qb.IsEqual(nameof(NotifyMessage.IsError), true.ToString())
                      .IsLessThanOrEqual(nameof(NotifyMessage.ProcessDate), errorPickupCutoffDate.ToString());
                }
                else
                {
                    qb.IsEqual(nameof(NotifyMessage.IsError), false.ToString());
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