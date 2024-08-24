namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a background task that sends notification messages to the MessageProcessQueue service.
    /// </summary>
    public static partial class NotificationSendTask
    {
        /// <summary>
        /// Queue the work to the background task queue.
        /// </summary>
        /// <param name="backgroundTaskQueue"></param>
        public static void QueueWork(this ITaskQueue backgroundTaskQueue)
        {
            backgroundTaskQueue.Queue(new Detail());
        }

        /// <summary>
        /// The detail class provides any additional information needed to perform the work.
        /// In this case, no additional information is needed.
        /// </summary>
        public class Detail : ITaskDetail<Detail, Worker>
        {
        }

        /// <summary>
        /// The worker class performs the work detail.
        /// </summary>
        public class Worker : ITaskWork<Detail, Worker>
        {
            private readonly INotifyMessageProcessQueueService _messageProcessQueueService;

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="messageProcessQueueService"></param>
            public Worker(
                INotifyMessageProcessQueueService messageProcessQueueService)
            {
                _messageProcessQueueService = messageProcessQueueService;
            }

            /// <summary>
            /// Perform the work.
            /// </summary>
            /// <param name="detail"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task DoWork(Detail detail, CancellationToken cancellationToken)
            {
                await _messageProcessQueueService.ExecuteAsync(cancellationToken);
            }
        }
    }
}