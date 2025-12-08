using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ServiceBricks.Notification
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class NotificationModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<NotificationModule>),
                typeof(NotificationModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<NotificationModule>),
                typeof(NotificationModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<NotificationModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            var config = e.Configuration;

            // AI: Configure all options for the module
            services.Configure<SendOptions>(config.GetSection(NotificationConstants.APPSETTINGS_SEND_OPTIONS));
            services.Configure<SmtpOptions>(config.GetSection(NotificationConstants.APPSETTINGS_SMTP_PROVIDER_OPTIONS));

            // AI: Add hosted services for the module
            if (config.GetSection(NotificationConstants.APPSETTINGS_SEND_OPTIONS).GetValue<bool>(nameof(SendOptions.TimerEnabled)))
                services.AddHostedService<SendNotificationTimer>();

            // AI: Add workers for tasks in the module
            services.AddScoped<SendNotificationTask.Worker>();
            services.AddScoped<NotifyMessageWorkService>();

            // AI: Add API Controllers for each DTO in the module
            services.AddScoped<IApiController<NotifyMessageDto>, NotifyMessageApiController>();
            services.AddScoped<INotifyMessageApiController, NotifyMessageApiController>();

            // AI: Add any miscellaneous services for the module
            var emailProvider = services.Where(x => x.ServiceType == typeof(IEmailProvider)).FirstOrDefault();
            if (emailProvider == null)
                services.AddScoped<IEmailProvider, ExampleEmailProvider>();
            var smsProvider = services.Where(x => x.ServiceType == typeof(ISmsProvider)).FirstOrDefault();
            if (smsProvider == null)
                services.AddScoped<ISmsProvider, ExampleSmsProvider>();

            // AI: Register mappings
            ApplicationEmailDtoMappingProfile.Register(MapperRegistry.Instance);
            ApplicationSmsDtoMappingProfile.Register(MapperRegistry.Instance);
            SenderTypeMappingProfile.Register(MapperRegistry.Instance);
            NotifyMessageMappingProfile.Register(MapperRegistry.Instance);

            // AI: Register business rules for the module
            SendNotificationProcessRule.Register(BusinessRuleRegistry.Instance);
            CreateApplicationEmailRule.Register(BusinessRuleRegistry.Instance);
            CreateApplicationSmsRule.Register(BusinessRuleRegistry.Instance);

            return response;
        }
    }
}