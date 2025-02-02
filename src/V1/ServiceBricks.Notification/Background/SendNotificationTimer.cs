using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a timer to execute the SendNotificationTask.
    /// Do not seal the class to allow for overriding values.
    /// </summary>
    public partial class SendNotificationTimer : TaskTimerHostedService<SendNotificationTask.Detail, SendNotificationTask.Worker>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public SendNotificationTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
        }

        /// <summary>
        /// The interval at which the timer will tick.
        /// </summary>
        public override TimeSpan TimerTickInterval
        {
            get { return TimeSpan.FromSeconds(10); }
        }

        /// <summary>
        /// The initial delay before the timer will tick.
        /// </summary>
        public override TimeSpan TimerDueTime
        {
            get { return TimeSpan.FromSeconds(1); }
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