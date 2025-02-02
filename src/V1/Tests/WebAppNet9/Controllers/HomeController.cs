using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceBricks;
using ServiceBricks.Notification;

using WebApp.ViewModel.Home;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        private IServiceBus _serviceBus;
        private IEmailProvider _emailProvider;
        private IBusinessRuleService _businessRuleService;
        private INotifyMessageApiService _notifyMessageApiService;

        public HomeController(
            IServiceBus serviceBus,
            IEmailProvider emailProvider,
            IBusinessRuleService businessRuleService,
            INotifyMessageApiService notifyMessageApiService)
        {
            _serviceBus = serviceBus;
            _emailProvider = emailProvider;
            _businessRuleService = businessRuleService;
            _notifyMessageApiService = notifyMessageApiService;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            return View(model);
        }

        [HttpGet]
        [Route("StartTest")]
        public async Task<IActionResult> StartTest()
        {
            for (int i = 0; i < 1000; i++)
            {
                NotifyMessageDto msg = new NotifyMessageDto()
                {
                    Body = "Test Body",
                    Subject = "Test Subject",
                    BodyHtml = "Test BodyHtml",
                    SenderType = SenderType.Email_TEXT,
                    ToAddress = "test@holomodular.com",
                };
                await _notifyMessageApiService.CreateAsync(msg);
            }

            HomeViewModel model = new HomeViewModel();
            return View("Index", model);
        }

        [HttpGet]
        [Route("SendGrid")]
        public async Task<IActionResult> SendGrid()
        {
            // Email
            NotifyMessageDto msg = new NotifyMessageDto()
            {
                Body = "Test Body",
                Subject = "Test Subject",
                BodyHtml = "Test BodyHtml",
                SenderType = SenderType.Email_TEXT,
                ToAddress = "support@servicebricks.com",
            };

            SendNotificationProcess process = new SendNotificationProcess(msg);
            var respProcess = await _businessRuleService.ExecuteProcessAsync(process);

            HomeViewModel model = new HomeViewModel();
            return View("Index", model);
        }

        [HttpGet]
        [Route("ServiceBus")]
        public IActionResult ServiceBus()
        {
            var log = new CreateApplicationLogBroadcast(new ApplicationLogDto()
            {
                Application = "ApplicationTest",
                CreateDate = DateTimeOffset.UtcNow,
                Category = "CategoryTest",
                Exception = "ExceptionTest",
                Level = "LevelTest",
                Message = "MessageTest",
                Path = "PathTest",
                Properties = "PropertiesTest",
                Server = "ServerTest",
                StorageKey = "StorageKeyTest",
                UserStorageKey = "UserStorageKeyTest",
            });
            _serviceBus.Send(log);

            var email = new CreateApplicationEmailBroadcast(new ApplicationEmailDto()
            {
                BccAddress = "BccAddressTest",
                BodyHtml = "BodyHtmlTest",
                CcAddress = "CcAddressTest",
                FromAddress = "FromAddressTest",
                FutureProcessDate = DateTimeOffset.UtcNow,
                IsHtml = true,
                Priority = "PriorityTest",
                ToAddress = "ToAddressTest",
                Body = "BodyTest",
                Subject = "SubjectTest",
                StorageKey = "StorageKeyTest",
            });
            _serviceBus.Send(email);

            var sms = new CreateApplicationSmsBroadcast(new ApplicationSmsDto()
            {
                Message = "MessageTest",
                PhoneNumber = "PhoneNumberTest",
                StorageKey = "StorageKeyTest",
                FutureProcessDate = DateTimeOffset.UtcNow,
            });
            _serviceBus.Send(sms);

            HomeViewModel model = new HomeViewModel();
            return View("Index", model);
        }

        [HttpGet]
        [Route("Error")]
        public IActionResult Error(string message = null)
        {
            var model = new ErrorViewModel()
            {
                Message = message
            };
            return View("Error", model);
        }
    }
}