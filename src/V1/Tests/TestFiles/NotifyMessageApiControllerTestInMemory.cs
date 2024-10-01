using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification;

namespace ServiceBricks.Xunit
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class NotifyMessageApiControllerTestInMemory : NotifyMessageApiControllerTest
    {
        public NotifyMessageApiControllerTestInMemory()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<NotifyMessageDto>>();
        }
    }
}