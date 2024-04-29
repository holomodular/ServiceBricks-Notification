using System.Threading;
using System.Threading.Tasks;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This allows processing from a database table like a queue.
    /// </summary>
    public interface INotifyMessageProcessQueueService
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}