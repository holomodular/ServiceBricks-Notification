using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Xunit;
using Newtonsoft.Json;
using ServiceQuery;
using Microsoft.AspNetCore.Mvc;
using static ServiceBricks.Xunit.ApiClientTests;
using ServiceBricks.Notification;
using Microsoft.Extensions.Configuration;

namespace ServiceBricks.Xunit
{
    public class NotifyMessageStubTestManager : NotifyMessageTestManager
    {
        public class NotifyMessageHttpClientFactory : IHttpClientFactory
        {
            private ApiClientTests.CustomGenericHttpClientHandler<NotifyMessageDto> _handler;

            public NotifyMessageHttpClientFactory(ApiClientTests.CustomGenericHttpClientHandler<NotifyMessageDto> handler)
            {
                _handler = handler;
            }

            public HttpClient CreateClient(string name)
            {
                return new HttpClient(_handler);
            }
        }

        public override IApiClient<NotifyMessageDto> GetClient(IServiceProvider serviceProvider)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var newconfig = new Dictionary<string, string>()
            {
                ["ServiceBricks:Notification:Client:Api:BaseServiceUrl"] = "https://localhost:7000/api/v1.0",
            };
            var config = configurationBuilder
                .AddInMemoryCollection(newconfig).Build();
            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            var apiservice = serviceProvider.GetRequiredService<INotifyMessageApiService>();
            var controller = new NotifyMessageApiController(apiservice, apioptions);
            var options = new OptionsWrapper<ClientApiOptions>(new ClientApiOptions() { ReturnResponseObject = false, BaseServiceUrl = "https://localhost:7000/", TokenUrl = "https://localhost:7000/token" });
            var handler = new ApiClientTests.CustomGenericHttpClientHandler<NotifyMessageDto>(controller);
            var clientHandlerFactory = new NotifyMessageHttpClientFactory(handler);
            return new NotifyMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }

        public ApiClientTests.CustomGenericHttpClientHandler<NotifyMessageDto> Handler { get; set; }

        public override IApiClient<NotifyMessageDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var newconfig = new Dictionary<string, string>()
            {
                ["ServiceBricks:Notification:Client:Api:BaseServiceUrl"] = "https://localhost:7000/api/v1.0",
                ["ServiceBricks:Notification:Client:Api:ReturnResponseObject"] = "true",
            };
            var config = configurationBuilder
                .AddInMemoryCollection(newconfig).Build();

            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true });
            var apiservice = serviceProvider.GetRequiredService<INotifyMessageApiService>();
            var controller = new NotifyMessageApiController(apiservice, apioptions);

            var handler = new ApiClientTests.CustomGenericHttpClientHandler<NotifyMessageDto>(controller);
            var clientHandlerFactory = new NotifyMessageHttpClientFactory(handler);
            return new NotifyMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubNotifyMessageApiClientTest : ApiClientTest<NotifyMessageDto>
    {
        public StubNotifyMessageApiClientTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new NotifyMessageStubTestManager();
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubNotifyMessageApiClientReturnResponseTests : ApiClientReturnResponseTest<NotifyMessageDto>
    {
        public StubNotifyMessageApiClientReturnResponseTests()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new NotifyMessageStubTestManager();
        }
    }
}