using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This business rule occurs when a CreateApplicationSMSBroadcast servicebus event is received.
    /// </summary>
    public sealed class CreateApplicationSmsRule : BusinessRule
    {
        private readonly INotifyMessageApiService _messageApiService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateApplicationSmsRule> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="messageApiService"></param>
        /// <param name="mapper"></param>
        public CreateApplicationSmsRule(
            ILoggerFactory loggerFactory,
            INotifyMessageApiService messageApiService,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<CreateApplicationSmsRule>();
            _mapper = mapper;
            _messageApiService = messageApiService;
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register the business rule.
        /// </summary>
        public static void Register(IServiceBus serviceBus)
        {
            serviceBus.Subscribe(
                typeof(CreateApplicationSmsBroadcast),
                typeof(CreateApplicationSmsRule));
        }

        /// <summary>
        /// UnRegister the business rule.
        /// </summary>
        public static void UnRegister(IServiceBus serviceBus)
        {
            serviceBus.UnSubscribe(
                typeof(CreateApplicationSmsBroadcast),
                typeof(CreateApplicationSmsRule));
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResponse ExecuteRule(IBusinessRuleContext context)
        {
            var response = new Response();

            try
            {
                // AI: Make sure the context object is the correct type
                var e = context.Object as CreateApplicationSmsBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // AI: Map to the DTO
                var message = _mapper.Map<NotifyMessageDto>(e.DomainObject);

                // AI: Call the API service to create the message
                var respCreate = _messageApiService.Create(message);

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
                var e = context.Object as CreateApplicationSmsBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // AI: Map to the DTO
                var message = _mapper.Map<NotifyMessageDto>(e.DomainObject);

                // AI: Call the API service to create the message
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