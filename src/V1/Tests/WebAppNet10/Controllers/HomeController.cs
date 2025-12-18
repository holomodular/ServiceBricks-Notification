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
        //private IEmailProvider _emailProvider;
        //private IBusinessRuleService _businessRuleService;
        //private INotifyMessageApiService _notifyMessageApiService;

        //public HomeController(
        //    IServiceBus serviceBus,
        //    IEmailProvider emailProvider,
        //    IBusinessRuleService businessRuleService,
        //    INotifyMessageApiService notifyMessageApiService)
        //{
        //    _serviceBus = serviceBus;
        //    _emailProvider = emailProvider;
        //    _businessRuleService = businessRuleService;
        //    _notifyMessageApiService = notifyMessageApiService;
        //}

        //[HttpGet]
        //[Route("StartTest")]
        //public async Task<IActionResult> StartTest()
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        NotifyMessageDto msg = new NotifyMessageDto()
        //        {
        //            Body = "Test Body",
        //            Subject = "Test Subject",
        //            BodyHtml = "Test BodyHtml",
        //            SenderType = SenderType.Email_TEXT,
        //            ToAddress = "test@holomodular.com",
        //        };
        //        await _notifyMessageApiService.CreateAsync(msg);
        //    }

        //    HomeViewModel model = new HomeViewModel();
        //    return View("Index", model);
        //}
     

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            return View(model);
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