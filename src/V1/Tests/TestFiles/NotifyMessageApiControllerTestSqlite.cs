﻿using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class NotifyMessageApiControllerTestSqlite : NotifyMessageApiControllerTest
    {
        public NotifyMessageApiControllerTestSqlite()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupSqlite));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<NotifyMessageDto>>();
        }
    }
}