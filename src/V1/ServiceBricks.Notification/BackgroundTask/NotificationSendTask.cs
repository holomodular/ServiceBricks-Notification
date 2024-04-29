using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a background task that sends notification messages.
    /// </summary>
    public static class NotificationSendTask
    {
        public static void QueueWork(this ITaskQueue backgroundTaskQueue)
        {
            backgroundTaskQueue.Queue(new Detail());
        }

        public class Detail : ITaskDetail<Detail, Worker>
        {
            public Detail()
            {
            }
        }

        public class Worker : ITaskWork<Detail, Worker>
        {
            private readonly ILogger<Worker> _logger;
            private readonly INotifyMessageProcessQueueService _messageProcessQueueService;

            public Worker(
                ILogger<Worker> logger,
                INotifyMessageProcessQueueService messageProcessQueueService)
            {
                _logger = logger;
                _messageProcessQueueService = messageProcessQueueService;
            }

            public async Task DoWork(Detail detail, CancellationToken cancellationToken)
            {
                await _messageProcessQueueService.ExecuteAsync(cancellationToken);
            }
        }
    }
}