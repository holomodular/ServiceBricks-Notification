using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace ServiceBricks.Notification.MongoDb
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class NotificationMongoDbModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<NotificationMongoDbModule>),
                typeof(NotificationMongoDbModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<NotificationMongoDbModule>),
                typeof(NotificationMongoDbModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<NotificationMongoDbModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            //var configuration = e.Configuration;

            // AI: Add any miscellaneous services for the module
            services.AddScoped<INotifyMessageProcessQueueService, NotifyMessageProcessQueueService>();

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<NotifyMessageDto>, NotifyMessageApiService>();
            services.AddScoped<INotifyMessageApiService, NotifyMessageApiService>();

            // AI: Add business rules for the module
            DomainCreateUpdateDateRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance, nameof(NotifyMessage.FutureProcessDate), nameof(NotifyMessage.ProcessDate));
            ApiConcurrencyByUpdateDateRule<NotifyMessage, NotifyMessageDto>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<NotifyMessage>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return response;
        }
    }
}