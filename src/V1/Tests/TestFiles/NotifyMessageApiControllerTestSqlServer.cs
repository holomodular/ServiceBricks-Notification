using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class NotifyMessageApiControllerTestSqlServer : NotifyMessageApiControllerTest
    {
        public NotifyMessageApiControllerTestSqlServer()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupSqlServer));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<NotifyMessageDto>>();
        }
    }
}