using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This business rule occurs when an Sms needs to be created.
    /// </summary>
    public partial class CreateApplicationSmsRule : BusinessRule
    {
        private readonly INotifyMessageApiService _messageApiService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateApplicationSmsRule> _logger;

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
        public static void RegisterServiceBus(IServiceBus serviceBus)
        {
            serviceBus.Subscribe(
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
                var e = context.Object as CreateApplicationSmsBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // Map and Create
                var message = _mapper.Map<NotifyMessageDto>(e.DomainObject);
                var respCreate = await _messageApiService.CreateAsync(message);
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