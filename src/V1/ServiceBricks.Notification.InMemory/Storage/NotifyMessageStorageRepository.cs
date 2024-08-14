using Microsoft.Extensions.Logging;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.InMemory
{
    /// <summary>
    /// This is a storage repository for the NotifyMessage domain object.
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
            NotificationInMemoryContext context) : base(loggerFactory, context)
        { }

        /// <summary>
        /// Get a list of queue items.
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