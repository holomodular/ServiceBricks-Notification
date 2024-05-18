using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.InMemory
{
    /// <summary>
    /// This is a storage repository for the notification message domain object.
    /// </summary>
    public class NotifyMessageStorageRepository : NotificationStorageRepository<NotifyMessage>, INotifyMessageStorageRepository
    {
        public NotifyMessageStorageRepository(
            ILoggerFactory loggerFactory,
            NotificationInMemoryContext context) : base(loggerFactory, context)
        { }

        public async Task<IResponseList<NotifyMessage>> GetQueueItemsAsync(int batchNumberToTake, bool pickupErrors, DateTimeOffset errorPickupCutoffDate)
        {
            ResponseList<NotifyMessage> response = new ResponseList<NotifyMessage>();
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                var inmemoryquery = DbSet.Where(x =>
                        !x.IsComplete &&
                        !x.IsProcessing &&
                        x.FutureProcessDate <= now);
                if (pickupErrors)
                    inmemoryquery = inmemoryquery.Where(x => x.IsError && x.ProcessDate <= errorPickupCutoffDate);
                else
                    inmemoryquery = inmemoryquery.Where(x => !x.IsError);
                var list = inmemoryquery.OrderBy(x => x.CreateDate).Take(batchNumberToTake).ToList();

                foreach (var item in list)
                {
                    item.IsProcessing = true;
                    item.ProcessDate = now;
                    item.UpdateDate = now;
                }

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