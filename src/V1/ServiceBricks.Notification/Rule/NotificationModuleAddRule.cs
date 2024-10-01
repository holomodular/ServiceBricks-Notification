using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            var configuration = e.Configuration;

            // AI: Add hosted services for the module
            services.AddHostedService<NotificationSendTimer>();

            // AI: Add workers for tasks in the module
            services.AddScoped<NotificationSendTask.Worker>();

            // AI: Configure all options for the module
            services.Configure<NotificationOptions>(configuration.GetSection(NotificationConstants.APPSETTINGS_NOTIFICATION_OPTIONS));
            services.Configure<SmtpOptions>(configuration.GetSection(NotificationConstants.APPSETTINGS_SMTP_PROVIDER_OPTIONS));

            // AI: Add API Controllers for each DTO in the module
            services.AddScoped<INotifyMessageApiController, NotifyMessageApiController>();

            // AI: Add any miscellaneous services for the module
            var emailProvider = services.Where(x => x.ServiceType == typeof(IEmailProvider)).FirstOrDefault();
            if (emailProvider == null)
                services.AddScoped<IEmailProvider, ExampleEmailProvider>();
            var smsProvider = services.Where(x => x.ServiceType == typeof(ISmsProvider)).FirstOrDefault();
            if (smsProvider == null)
                services.AddScoped<ISmsProvider, ExampleSmsProvider>();

            // AI: Register business rules for the module
            SendNotificationProcessRule.Register(BusinessRuleRegistry.Instance);
            CreateApplicationEmailRule.Register(BusinessRuleRegistry.Instance);
            CreateApplicationSmsRule.Register(BusinessRuleRegistry.Instance);

            return response;
        }
    }
}