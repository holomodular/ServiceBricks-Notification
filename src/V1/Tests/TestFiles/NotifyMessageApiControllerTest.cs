﻿using Microsoft.AspNetCore.Mvc;
using ServiceBricks.Notification;

namespace ServiceBricks.Xunit
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public abstract class NotifyMessageApiControllerTest : ApiControllerTest<NotifyMessageDto>
    {
        [Fact]
        public virtual async Task Update_CreateDate()
        {
            var model = TestManager.GetMinimumDataObject();
            var dto = await CreateBaseAsync(model);

            DateTimeOffset startingCreateDate = dto.CreateDate;

            //Update the CreateDate property
            dto.CreateDate = DateTime.UtcNow;

            //Call Update
            var controller = TestManager.GetController(SystemManager.ServiceProvider);
            var respUpdate = await controller.UpdateAsync(dto);
            if (respUpdate is OkObjectResult okResult)
            {
                Assert.True(okResult.Value != null);
                if (okResult.Value is NotifyMessageDto obj)
                {
                    Assert.True(obj.CreateDate == startingCreateDate);
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