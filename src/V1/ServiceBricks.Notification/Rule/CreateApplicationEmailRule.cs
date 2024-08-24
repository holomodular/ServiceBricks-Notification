using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This business rule occurs when an CreateApplicationEmailBroadcast is received from the service bus.
    /// </summary>
    public sealed class CreateApplicationEmailRule : BusinessRule
    {
        private readonly INotifyMessageApiService _messageApiService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateApplicationEmailRule> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="messageApiService"></param>
        /// <param name="mapper"></param>
        public CreateApplicationEmailRule(
            ILoggerFactory loggerFactory,
            INotifyMessageApiService messageApiService,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<CreateApplicationEmailRule>();
            _mapper = mapper;
            _messageApiService = messageApiService;
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register the business rule.
        /// </summary>
        public static void RegisterServiceBus(IServiceBus serviceBus)
        {
            serviceBus.Subscribe(
                typeof(CreateApplicationEmailBroadcast),
                typeof(CreateApplicationEmailRule));
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResponse ExecuteRule(IBusinessRuleContext context)
        {
            return ExecuteRuleAsync(context).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            var response = new Response();

            try
            {
                // AI: Make sure the context object is the correct type
                var e = context.Object as CreateApplicationEmailBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // AI: Map the domain object to the DTO
                var message = _mapper.Map<NotifyMessageDto>(e.DomainObject);

                // AI: Call the API service to store the message
                var respCreate = await _messageApiService.CreateAsync(message);

                // AI: Copy the API response to the business rule response
                response.CopyFrom(respCreate);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
                return response;
            }
        }
    }
}