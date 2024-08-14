using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a timer to execute the NotificationSendTask.
    /// Do not seal the class to allow for overriding values.
    /// </summary>
    public class NotificationSendTimer : TaskTimerHostedService<NotificationSendTask.Detail, NotificationSendTask.Worker>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public NotificationSendTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
        }

        /// <summary>
        /// The interval at which the timer will tick.
        /// </summary>
        public override TimeSpan TimerTickInterval
        {
            get { return TimeSpan.FromSeconds(30); }
        }

        /// <summary>
        /// The initial delay before the timer will tick.
        /// </summary>
        public override TimeSpan TimerDueTime
        {
            get { return TimeSpan.FromSeconds(30); }
        }

        /// <summary>
        /// The task detail for the timer that will be executed.
        /// </summary>
        public override ITaskDetail<NotificationSendTask.Detail, NotificationSendTask.Worker> TaskDetail
        {
            get { return new NotificationSendTask.Detail(); }
        }

        /// <summary>
        /// Determine if the timer should process the run.
        /// </summary>
        /// <returns></returns>
        public override bool TimerTickShouldProcessRun()
        {
            // If scaling the application, consider using the Cache SingleServerProcess to ensure only one instance runs.
            return ApplicationBuilderExtensions.ModuleStarted &&
                !IsCurrentlyRunning;
        }
    }
}