using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This is an exposed REST API controller for the Message domain object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Notification/NotifyMessage")]
    [Produces("application/json")]
    public class NotifyMessageApiController : AdminPolicyApiController<NotifyMessageDto>, INotifyMessageApiController
    {
        private readonly INotifyMessageApiService _MessageApiService;

        public NotifyMessageApiController(
            INotifyMessageApiService MessageApiService,
            IOptions<ApiOptions> apiOptions)
            : base(MessageApiService, apiOptions)
        {
            _MessageApiService = MessageApiService;
        }

        //[HttpGet]
        //[Route("GetSenderTypes")]
        //[ProducesResponseType(typeof(List<DomainTypeDto>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        //[Authorize(Policy = ServiceBricksConstants.AdminSecurityPolicyName)]
        //public virtual async Task<ActionResult> GetSenderTypesAsync()
        //{
        //    var resp = await _MessageApiService.GetSenderTypesAsync();
        //    if (resp.Success)
        //        return Ok(resp.List);
        //    return GetErrorResponse(resp);
        //}
    }
}