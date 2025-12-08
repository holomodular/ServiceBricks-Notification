using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a timer to execute the SendNotificationTask.
    /// Do not seal the class to allow for overriding values.
    /// </summary>
    public partial class SendNotificationTimer : TaskTimerHostedService<SendNotificationTask.Detail, SendNotificationTask.Worker>
    {
        private SendOptions _sendOptions;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="sendOptions"></param>
        public SendNotificationTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger,
            IOptions<SendOptions> sendOptions) : base(serviceProvider, logger)
        {
            _sendOptions = sendOptions.Value;
            TimerTickInterval = TimeSpan.FromMilliseconds(_sendOptions.TimerIntervalMilliseconds);
            TimerDueTime = TimeSpan.FromMilliseconds(_sendOptions.TimerDueMilliseconds);
        }

        /// <summary>
        /// The task detail for the timer that will be executed.
        /// </summary>
        public override ITaskDetail<SendNotificationTask.Detail, SendNotificationTask.Worker> TaskDetail
        {
            get { return new SendNotificationTask.Detail(); }
        }

        /// <summary>
        /// Determine if the timer should process the run.
        /// </summary>
        /// <returns></returns>
        public override bool TimerTickShouldProcessRun()
        {
            return NotificationModule.Instance.Started && !IsCurrentlyRunning;
        }
    }
}