using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class NotificationCosmosModuleStartRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleStartEvent<NotificationCosmosModule>),
                typeof(NotificationCosmosModuleStartRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleStartEvent<NotificationCosmosModule>),
                typeof(NotificationCosmosModuleStartRule));
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
            var e = context.Object as ModuleStartEvent<NotificationCosmosModule>;
            if (e == null || e.DomainObject == null || e.ApplicationBuilder == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic

            // AI: Ensure the database is created
            using (var scope = e.ApplicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<NotificationCosmosContext>();
                dbcontext.Database.EnsureCreated();
            }

            return response;
        }
    }
}