using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class NotifyMessageApiControllerTestAzureDataTables : NotifyMessageApiControllerTest
    {
        public NotifyMessageApiControllerTestAzureDataTables()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupAzureDataTables));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<NotifyMessageDto>>();
        }

        [Fact]
        public virtual async Task MinimumDateTest()
        {
            var model = TestManager.GetMinimumDataObject();
            model.FutureProcessDate = DateTimeOffset.MinValue;
            model.ProcessDate = DateTimeOffset.MinValue;

            var dto = await CreateBaseAsync(model);

            // Set to min dates
            dto.FutureProcessDate = DateTimeOffset.MinValue;
            dto.ProcessDate = DateTimeOffset.MinValue;

            //Call Update
            var controller = TestManager.GetController(SystemManager.ServiceProvider);
            var respUpdate = await controller.UpdateAsync(dto);
            if (respUpdate is OkObjectResult okResult)
            {
                Assert.True(okResult.Value != null);
                if (okResult.Value is NotifyMessageDto obj)
                {
                    Assert.True(obj.FutureProcessDate == StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE);
                    Assert.True(obj.ProcessDate == StorageAzureDataTablesConstants.DATETIMEOFFSET_MINDATE);
                }
                else
                    Assert.Fail("");
            }
            else
                Assert.Fail("");

            // Cleanup
            await DeleteBaseAsync(dto);
        }
    }
}