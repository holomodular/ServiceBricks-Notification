using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class NotificationAzureDataTablesModuleStartRule : BusinessRule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationAzureDataTablesModuleStartRule()
        {
        }

        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleStartEvent<NotificationAzureDataTablesModule>),
                typeof(NotificationAzureDataTablesModuleStartRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleStartEvent<NotificationAzureDataTablesModule>),
                typeof(NotificationAzureDataTablesModuleStartRule));
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
            var e = context.Object as ModuleStartEvent<NotificationAzureDataTablesModule>;
            if (e == null || e.DomainObject == null || e.ApplicationBuilder == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic

            // AI: Get the connection string
            var configuration = e.ApplicationBuilder.ApplicationServices.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetAzureDataTablesConnectionString(
                NotificationAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);

            // AI: Create each table in the module
            TableClient tableClient = new TableClient(
                connectionString,
                NotificationAzureDataTablesConstants.GetTableName(nameof(NotifyMessage)));
            tableClient.CreateIfNotExists();

            return response;
        }
    }
}