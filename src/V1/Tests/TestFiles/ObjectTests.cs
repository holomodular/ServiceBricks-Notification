using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Notification;

namespace ServiceBricks.Xunit
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class ObjectTests
    {
        public virtual ISystemManager SystemManager { get; set; }

        public ObjectTests()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
        }

        [Fact]
        public virtual async Task NotificationSendTaskTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            var apiservice = SystemManager.ServiceProvider.GetRequiredService<INotifyMessageApiService>();

            var startdto = new NotifyMessageDto()
            {
                SenderType = SenderType.Email_TEXT,
                Body = "test",
                ToAddress = "test@servicebricks.com"
            };

            var respcreate = apiservice.Create(startdto);
            Assert.True(respcreate.Success);
            Assert.True(respcreate.Item != null);

            var respg = apiservice.Get(respcreate.Item.StorageKey);
            Assert.True(respg.Success);
            Assert.True(respg.Item != null);

            NotificationSendTask.QueueWork(taskQueue);
            NotificationSendTask.Worker worker = new NotificationSendTask.Worker(
                SystemManager.ServiceProvider.GetRequiredService<INotifyMessageProcessQueueService>());

            await worker.DoWork(new NotificationSendTask.Detail(), CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            while (!apiservice.Get(respcreate.Item.StorageKey).Item.IsComplete)
            {
                if (cts.Token.IsCancellationRequested)
                    break;
            }

            respg = apiservice.Get(respcreate.Item.StorageKey);

            Assert.True(respg.Item.IsComplete);
        }

        [Fact]
        public virtual async Task NotificationSendTimerTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            var apiservice = SystemManager.ServiceProvider.GetRequiredService<INotifyMessageApiService>();

            var startdto = new NotifyMessageDto()
            {
                SenderType = SenderType.Email_TEXT,
                Body = "test",
                ToAddress = "test@servicebricks.com"
            };

            var respcreate = apiservice.Create(startdto);
            Assert.True(respcreate.Success);
            Assert.True(respcreate.Item != null);

            var respg = apiservice.Get(respcreate.Item.StorageKey);
            Assert.True(respg.Success);
            Assert.True(respg.Item != null);

            var timer = new NotificationSendTimer(
                SystemManager.ServiceProvider,
                SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>());

            await timer.StartAsync(CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            while (!apiservice.Get(respcreate.Item.StorageKey).Item.IsComplete)
            {
                if (cts.Token.IsCancellationRequested)
                    break;
            }

            await timer.StopAsync(CancellationToken.None);

            respg = apiservice.Get(respcreate.Item.StorageKey);

            Assert.True(respg.Item.IsComplete);
        }

        [Fact]
        public virtual async Task ExampleEmailProviderTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();

            ExampleEmailProvider provider = new ExampleEmailProvider(loggerFactory);

            await provider.SendEmailAsync(new NotifyMessageDto()
            {
                Body = "test",
                Subject = "test",
                ToAddress = "test@servicebricks.com,test2@servicebricks.com",
                CcAddress = "test3@servicebricks.com,test4@servicebricks.com",
                BccAddress = "test5@servicebricks.com,test6@servicebricks.com",
            });
        }

        [Fact]
        public virtual async Task ExampleSmsProviderTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();

            ExampleSmsProvider provider = new ExampleSmsProvider(loggerFactory);

            await provider.SendSmsAsync(new NotifyMessageDto()
            {
                Body = "test",
                ToAddress = "1234567890",
            });
        }

        [Fact]
        public virtual async Task SendNotificationProcessTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var businessRuleService = SystemManager.ServiceProvider.GetRequiredService<IBusinessRuleService>();

            NotificationSendProcess process = new NotificationSendProcess(new NotifyMessageDto()
            {
                ToAddress = "test1@servicebricks.com,test2@servicebricks.com",
                CcAddress = "test3@servicebricks.com,test4@servicebricks.com",
                BccAddress = "test5@servicebricks.com,test6@servicebricks.com",
                Body = "test",
                BodyHtml = "test",
                Subject = "test",
                Priority = "low",
                IsHtml = true,
                SenderType = SenderType.Email_TEXT
            });

            var resp = await businessRuleService.ExecuteProcessAsync(process);
        }

        [Fact]
        public virtual Task AddNotificationTests()
        {
            IServiceCollection services = new ServiceCollection();
            var config = new ConfigurationBuilder().Build();

            services.AddServiceBricks(config);
            services.AddServiceBricksNotification(config);

            return Task.CompletedTask;
        }

        [Fact]
        public virtual async Task CreateApplicationEmailRuleTests()
        {
            CreateApplicationEmailRule rule = new CreateApplicationEmailRule(
                SystemManager.ServiceProvider.GetRequiredService<INotifyMessageApiService>(),
                SystemManager.ServiceProvider.GetRequiredService<IMapper>());

            BusinessRuleContext context = new BusinessRuleContext();
            context.Object = new CreateApplicationEmailBroadcast(new ApplicationEmailDto()
            {
                ToAddress = "test1@servicebricks.com,test2@servicebricks.com",
                CcAddress = "test3@servicebricks.com,test4@servicebricks.com",
                BccAddress = "test5@servicebricks.com,test6@servicebricks.com",
                Body = "test",
                BodyHtml = "test",
                Subject = "test",
                Priority = "normal",
                IsHtml = true,
            });

            rule.ExecuteRule(context);

            await rule.ExecuteRuleAsync(context);
        }

        [Fact]
        public virtual async Task CreateApplicationSmsRuleTests()
        {
            CreateApplicationSmsRule rule = new CreateApplicationSmsRule(
                SystemManager.ServiceProvider.GetRequiredService<INotifyMessageApiService>(),
                SystemManager.ServiceProvider.GetRequiredService<IMapper>());

            BusinessRuleContext context = new BusinessRuleContext();
            context.Object = new CreateApplicationSmsBroadcast(new ApplicationSmsDto()
            {
                Message = "test",
                PhoneNumber = "1234567890",
            });

            rule.ExecuteRule(context);

            await rule.ExecuteRuleAsync(context);
        }

        [Fact]
        public virtual Task SmtpOptionsTests()
        {
            SmtpOptions options = new SmtpOptions()
            {
                EmailEnableSsl = false,
                EmailPassword = "test",
                EmailPort = 25,
                EmailServer = "test",
                EmailUsername = "test"
            };

            return Task.CompletedTask;
        }

        [Fact]
        public virtual Task ModuleTests()
        {
            NotificationModule module = new NotificationModule();

            var dep = module.DependentModules;
            var au = module.AutomapperAssemblies;
            var vi = module.ViewAssemblies;

            return Task.CompletedTask;
        }

        [Fact]
        public virtual async Task SmtpEmailProviderTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();

            var options = new OptionsWrapper<SmtpOptions>(new SmtpOptions()
            {
                EmailEnableSsl = false,
                EmailPassword = "test",
                EmailPort = 25,
                EmailServer = "test",
                EmailUsername = "test"
            });
            SmtpEmailProvider provider = new SmtpEmailProvider(
                loggerFactory,
                options);

            await provider.SendEmailAsync(new NotifyMessageDto()
            {
                ToAddress = "test1@servicebricks.com,test2@servicebricks.com",
                CcAddress = "test3@servicebricks.com,test4@servicebricks.com",
                BccAddress = "test5@servicebricks.com,test6@servicebricks.com",
                Body = "test",
                BodyHtml = "test",
                Subject = "test",
                Priority = "high",
                IsHtml = true,
            });
        }

        [Fact]
        public virtual Task SenderTypeTests()
        {
            SenderType senderType = new SenderType();
            senderType.Name = "test";
            senderType.Key = "test";

            var list = SenderType.GetAll();
            Assert.True(list.Count == 2);

            return Task.CompletedTask;
        }
    }
}