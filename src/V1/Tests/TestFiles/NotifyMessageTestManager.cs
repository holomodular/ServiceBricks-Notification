using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Notification;
using ServiceQuery;

namespace ServiceBricks.Xunit
{
    public class MongoDbNotifyMessageTestManager : NotifyMessageTestManager
    {
        public override NotifyMessageDto GetObjectNotFound()
        {
            return new NotifyMessageDto()
            {
                StorageKey = "000000000000000000000000"
            };
        }
    }

    public class NotifyMessageTestManagerPostgres : NotifyMessageTestManager
    {
        public override void ValidateObjects(NotifyMessageDto clientDto, NotifyMessageDto serviceDto, HttpMethod method)
        {
            //CreateDateRule
            if (method == HttpMethod.Post)
                Assert.True(serviceDto.CreateDate >= clientDto.CreateDate); //Rule
            else if (method == HttpMethod.Get)
            {
                // Postgres special handling
                long utcTicks = serviceDto.CreateDate.UtcTicks;
                utcTicks = ((long)(utcTicks / 10)) * 10;
                Assert.True(utcTicks == clientDto.CreateDate.UtcTicks);
            }
            else
                Assert.True(serviceDto.CreateDate == clientDto.CreateDate);

            //PrimaryKeyRule
            if (method == HttpMethod.Post)
                Assert.True(!string.IsNullOrEmpty(serviceDto.StorageKey));
            else
                Assert.True(serviceDto.StorageKey == clientDto.StorageKey);

            Assert.True(serviceDto.BccAddress == clientDto.BccAddress);

            Assert.True(serviceDto.Body == clientDto.Body);

            Assert.True(serviceDto.CcAddress == clientDto.CcAddress);

            Assert.True(serviceDto.FromAddress == clientDto.FromAddress);

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (clientDto.FutureProcessDate != DateTimeOffset.MinValue)
                {
                    long utcTicks = clientDto.FutureProcessDate.UtcTicks;
                    utcTicks = ((long)(utcTicks / 10)) * 10;
                    Assert.True(utcTicks == serviceDto.FutureProcessDate.UtcTicks);
                    //Assert.True(serviceDto.FutureProcessDate == clientDto.FutureProcessDate);
                }
            }
            else
            {
                long utcTicks = serviceDto.FutureProcessDate.UtcTicks;
                utcTicks = ((long)(utcTicks / 10)) * 10;
                Assert.True(utcTicks == clientDto.FutureProcessDate.UtcTicks);
                //Assert.True(serviceDto.FutureProcessDate == clientDto.FutureProcessDate);
            }

            Assert.True(serviceDto.IsComplete == clientDto.IsComplete);

            Assert.True(serviceDto.IsError == clientDto.IsError);

            Assert.True(serviceDto.IsHtml == clientDto.IsHtml);

            Assert.True(serviceDto.IsProcessing == clientDto.IsProcessing);

            Assert.True(serviceDto.Priority == clientDto.Priority);

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (clientDto.ProcessDate != DateTimeOffset.MinValue)
                {
                    long utcTicks = clientDto.ProcessDate.UtcTicks;
                    utcTicks = ((long)(utcTicks / 10)) * 10;
                    Assert.True(utcTicks == serviceDto.ProcessDate.UtcTicks);
                    //Assert.True(serviceDto.ProcessDate == clientDto.ProcessDate);
                }
            }
            else
            {
                long utcTicks = serviceDto.FutureProcessDate.UtcTicks;
                utcTicks = ((long)(utcTicks / 10)) * 10;
                Assert.True(utcTicks == clientDto.FutureProcessDate.UtcTicks);
                //Assert.True(serviceDto.ProcessDate == clientDto.ProcessDate);
            }

            Assert.True(serviceDto.RetryCount == clientDto.RetryCount);

            Assert.True(serviceDto.SenderType == clientDto.SenderType);

            Assert.True(serviceDto.Subject == clientDto.Subject);

            Assert.True(serviceDto.ToAddress == clientDto.ToAddress);

            //UpdateDateRule
            if (method == HttpMethod.Post || method == HttpMethod.Put)
                Assert.True(serviceDto.UpdateDate > clientDto.UpdateDate); //Rule
            else
            {
                // Postgres special handling
                long utcTicks = serviceDto.UpdateDate.UtcTicks;
                utcTicks = ((long)(utcTicks / 10)) * 10;
                Assert.True(utcTicks == clientDto.UpdateDate.UtcTicks);
            }
        }
    }

    public class NotifyMessageTestManager : TestManager<NotifyMessageDto>
    {
        public override NotifyMessageDto GetMinimumDataObject()
        {
            return new NotifyMessageDto()
            {
                SenderType = SenderType.Email_TEXT //MessageRule
            };
        }

        public override NotifyMessageDto GetMaximumDataObject()
        {
            var model = new NotifyMessageDto()
            {
                CreateDate = DateTimeOffset.UtcNow,
                BccAddress = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString(),
                BodyHtml = Guid.NewGuid().ToString(),
                CcAddress = Guid.NewGuid().ToString(),
                FromAddress = Guid.NewGuid().ToString(),
                FutureProcessDate = DateTimeOffset.UtcNow.AddDays(1),
                IsComplete = false,
                IsError = false,
                IsHtml = false,
                IsProcessing = false,
                Priority = Guid.NewGuid().ToString(),
                ProcessDate = DateTimeOffset.UtcNow.AddDays(-1),
                RetryCount = 1,
                SenderType = SenderType.Email_TEXT, //MessageRule
                Subject = Guid.NewGuid().ToString(),
                ToAddress = Guid.NewGuid().ToString(),
                UpdateDate = DateTimeOffset.UtcNow
            };
            return model;
        }

        public override IApiController<NotifyMessageDto> GetController(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            return new NotifyMessageApiController(serviceProvider.GetRequiredService<INotifyMessageApiService>(), options);
        }

        public override IApiController<NotifyMessageDto> GetControllerReturnResponse(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true });
            return new NotifyMessageApiController(serviceProvider.GetRequiredService<INotifyMessageApiService>(), options);
        }

        public override IApiClient<NotifyMessageDto> GetClient(IServiceProvider serviceProvider)
        {
            return new NotifyMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                serviceProvider.GetRequiredService<IConfiguration>());
        }

        public override IApiClient<NotifyMessageDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            return new NotifyMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                serviceProvider.GetRequiredService<IConfiguration>());
        }

        public override IApiService<NotifyMessageDto> GetService(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<INotifyMessageApiService>();
        }

        public override void UpdateObject(NotifyMessageDto dto)
        {
            dto.BccAddress = Guid.NewGuid().ToString();
            dto.Body = Guid.NewGuid().ToString();
            dto.CcAddress = Guid.NewGuid().ToString();
            dto.FromAddress = Guid.NewGuid().ToString();
            dto.FutureProcessDate = DateTimeOffset.UtcNow.AddDays(1);
            dto.IsComplete = true;
            dto.IsError = true;
            dto.IsHtml = true;
            dto.IsProcessing = true;
            dto.Priority = Guid.NewGuid().ToString();
            dto.ProcessDate = DateTimeOffset.UtcNow.AddDays(-1);
            dto.RetryCount = 2;
            dto.SenderType = SenderType.SMS_TEXT; //MessageRule
            dto.Subject = Guid.NewGuid().ToString();
            dto.ToAddress = Guid.NewGuid().ToString();
        }

        public override void ValidateObjects(NotifyMessageDto clientDto, NotifyMessageDto serviceDto, HttpMethod method)
        {
            //CreateDateRule
            if (method == HttpMethod.Post)
                Assert.True(serviceDto.CreateDate >= clientDto.CreateDate); //Rule
            else
                Assert.True(serviceDto.CreateDate == clientDto.CreateDate);

            //PrimaryKeyRule
            if (method == HttpMethod.Post)
                Assert.True(!string.IsNullOrEmpty(serviceDto.StorageKey));
            else
                Assert.True(serviceDto.StorageKey == clientDto.StorageKey);

            Assert.True(serviceDto.BccAddress == clientDto.BccAddress);

            Assert.True(serviceDto.Body == clientDto.Body);

            Assert.True(serviceDto.CcAddress == clientDto.CcAddress);

            Assert.True(serviceDto.FromAddress == clientDto.FromAddress);

            if (method == HttpMethod.Post)
            {
                if (clientDto.FutureProcessDate != DateTimeOffset.MinValue)
                    Assert.True(serviceDto.FutureProcessDate == clientDto.FutureProcessDate);
            }
            else
                Assert.True(serviceDto.FutureProcessDate == clientDto.FutureProcessDate);

            Assert.True(serviceDto.IsComplete == clientDto.IsComplete);

            Assert.True(serviceDto.IsError == clientDto.IsError);

            Assert.True(serviceDto.IsHtml == clientDto.IsHtml);

            Assert.True(serviceDto.IsProcessing == clientDto.IsProcessing);

            Assert.True(serviceDto.Priority == clientDto.Priority);

            if (method == HttpMethod.Post)
            {
                if (clientDto.ProcessDate != DateTimeOffset.MinValue)
                    Assert.True(serviceDto.ProcessDate == clientDto.ProcessDate);
            }
            else
                Assert.True(serviceDto.ProcessDate == clientDto.ProcessDate);

            Assert.True(serviceDto.RetryCount == clientDto.RetryCount);

            Assert.True(serviceDto.SenderType == clientDto.SenderType);

            Assert.True(serviceDto.Subject == clientDto.Subject);

            Assert.True(serviceDto.ToAddress == clientDto.ToAddress);

            //UpdateDateRule
            if (method == HttpMethod.Post || method == HttpMethod.Put)
                Assert.True(serviceDto.UpdateDate > clientDto.UpdateDate); //Rule
            else
                Assert.True(serviceDto.UpdateDate == clientDto.UpdateDate);
        }

        public override List<ServiceQueryRequest> GetQueriesForObject(NotifyMessageDto dto)
        {
            List<ServiceQueryRequest> queries = new List<ServiceQueryRequest>();

            var qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.BccAddress), dto.BccAddress);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.Body), dto.Body);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.BodyHtml), dto.BodyHtml);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.CcAddress), dto.CcAddress);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.CreateDate), dto.CreateDate.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.FromAddress), dto.FromAddress);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.FutureProcessDate), dto.FutureProcessDate.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.IsComplete), dto.IsComplete.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.IsError), dto.IsError.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.IsHtml), dto.IsHtml.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.IsProcessing), dto.IsProcessing.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.Priority), dto.Priority);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.ProcessDate), dto.ProcessDate.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.RetryCount), dto.RetryCount.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.SenderType), dto.SenderType);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.StorageKey), dto.StorageKey);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.Subject), dto.Subject);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.ToAddress), dto.ToAddress);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(NotifyMessageDto.UpdateDate), dto.UpdateDate.ToString("o"));
            queries.Add(qb.Build());

            return queries;
        }
    }
}