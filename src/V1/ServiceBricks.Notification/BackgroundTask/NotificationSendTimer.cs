using Microsoft.Extensions.Logging;
using System;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is a timer to execute the background task to send notification messages.
    /// </summary>
    public class NotificationSendTimer : TaskTimerHostedService<NotificationSendTask.Detail, NotificationSendTask.Worker>
    {
        public NotificationSendTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
        }

        public override TimeSpan TimerTickInterval
        {
            get { return TimeSpan.FromSeconds(30); }
        }

        public override ITaskDetail<NotificationSendTask.Detail, NotificationSendTask.Worker> TaskDetail
        {
            get { return new NotificationSendTask.Detail(); }
        }

        public override TimeSpan TimerDueTime
        {
            get { return TimeSpan.FromSeconds(30); }
        }

        public override bool TimerTickShouldProcessRun()
        {
            return ApplicationBuilderExtensions.ModuleStarted &&
                !IsCurrentlyRunning;
        }
    }
}