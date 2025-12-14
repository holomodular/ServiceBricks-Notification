using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Notification;

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
            var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":DisableAuthentication", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":TokenUrl", "https://localhost:7000/token" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":BaseServiceUrl", "https://localhost:7000/" },
            })
            .Build();

            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            var apiservice = serviceProvider.GetRequiredService<INotifyMessageApiService>();
            var controller = new NotifyMessageApiController(apiservice, apioptions);
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
            var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "true" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":DisableAuthentication", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":TokenUrl", "https://localhost:7000/token" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":BaseServiceUrl", "https://localhost:7000/" },
            })
            .Build();

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