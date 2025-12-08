namespace ServiceBricks.Notification
{
    /// <summary>
    /// This business rule occurs when an CreateApplicationEmailBroadcast is received from the service bus.
    /// </summary>
    public sealed class CreateApplicationEmailRule : BusinessRule
    {
        private readonly INotifyMessageApiService _messageApiService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageApiService"></param>
        /// <param name="mapper"></param>
        public CreateApplicationEmailRule(
            INotifyMessageApiService messageApiService,
            IMapper mapper)
        {
            _mapper = mapper;
            _messageApiService = messageApiService;
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register the business rule.
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(CreateApplicationEmailBroadcast),
                typeof(CreateApplicationEmailRule));
        }

        /// <summary>
        /// UnRegister the business rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
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
            var response = new Response();

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var e = context.Object as CreateApplicationEmailBroadcast;
            if (e == null || e.DomainObject == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Map the domain object to the DTO
            var message = _mapper.Map<ApplicationEmailDto, NotifyMessageDto>(e.DomainObject);

            // AI: Call the API service to store the message
            var respCreate = _messageApiService.Create(message);

            // AI: Copy the API response to the business rule response
            response.CopyFrom(respCreate);

            return response;
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            var response = new Response();

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var e = context.Object as CreateApplicationEmailBroadcast;
            if (e == null || e.DomainObject == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Map the domain object to the DTO
            var message = _mapper.Map<ApplicationEmailDto, NotifyMessageDto>(e.DomainObject);

            // AI: Call the API service to store the message
            var respCreate = await _messageApiService.CreateAsync(message);

            // AI: Copy the API response to the business rule response
            response.CopyFrom(respCreate);

            return response;
        }
    }
}