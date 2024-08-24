using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an exposed REST API controller for the NotifyMessageDto data transfer object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Notification/NotifyMessage")]
    [Produces("application/json")]
    public partial class NotifyMessageApiController : AdminPolicyApiController<NotifyMessageDto>, INotifyMessageApiController
    {
        protected readonly INotifyMessageApiService _notifyMessageApiService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageApiService"></param>
        /// <param name="apiOptions"></param>
        public NotifyMessageApiController(
            INotifyMessageApiService notifyMessageApiService,
            IOptions<ApiOptions> apiOptions)
            : base(notifyMessageApiService, apiOptions)
        {
            _notifyMessageApiService = notifyMessageApiService;
        }
    }
}