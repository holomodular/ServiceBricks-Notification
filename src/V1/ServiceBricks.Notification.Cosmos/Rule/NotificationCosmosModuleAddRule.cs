using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class NotificationCosmosModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<NotificationCosmosModule>),
                typeof(NotificationCosmosModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<NotificationCosmosModule>),
                typeof(NotificationCosmosModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<NotificationCosmosModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            var configuration = e.Configuration;

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<NotificationCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                NotificationCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(
                NotificationCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<NotificationCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationCosmosContext>>(builder.Options);
            services.AddDbContext<NotificationCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Storage Services for the module for each domain object
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiService>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiService>();

            // AI: Register business rules for the module
            DomainCreateUpdateDateRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance,
                nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Key");
            NotifyMessageCreateRule.Register(BusinessRuleRegistry.Instance);

            // AI: Add any miscellaneous services for the module
            services.AddScoped<INotifyMessageProcessQueueService, NotifyMessageProcessQueueService>();

            return response;
        }
    }
}