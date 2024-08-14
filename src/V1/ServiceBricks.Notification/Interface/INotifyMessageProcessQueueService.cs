namespace ServiceBricks.Notification
{
    /// <summary>
    /// This allows processing from a database table like a queue.
    /// </summary>
    public partial interface INotifyMessageProcessQueueService
    {
        /// <summary>
        /// Execute the process queue.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}