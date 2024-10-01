using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class NotifyMessageApiControllerTestCosmos : NotifyMessageApiControllerTest
    {
        public NotifyMessageApiControllerTestCosmos()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupPostgres));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<NotifyMessageDto>>();
        }
    }
}