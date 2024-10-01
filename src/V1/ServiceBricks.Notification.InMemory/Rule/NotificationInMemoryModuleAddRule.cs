using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.InMemory
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class NotificationInMemoryModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<NotificationInMemoryModule>),
                typeof(NotificationInMemoryModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<NotificationInMemoryModule>),
                typeof(NotificationInMemoryModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<NotificationInMemoryModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            var configuration = e.Configuration;

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<NotificationInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString(), b => b.EnableNullChecks(false));
            services.Configure<DbContextOptions<NotificationInMemoryContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationInMemoryContext>>(builder.Options);
            services.AddDbContext<NotificationInMemoryContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            return response;
        }
    }
}