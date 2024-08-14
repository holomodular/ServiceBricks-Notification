using Azure.Data.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// Extension methods for starting the ServiceBricks Notification Azure Data Tables module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to determine if the module has started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Notification Azure Data Tables module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksNotificationAzureDataTables(this IApplicationBuilder applicationBuilder)
        {
            // AI: Get the connection string
            var configuration = applicationBuilder.ApplicationServices.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetAzureDataTablesConnectionString(
                NotificationAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);

            // AI: Create each table in the module
            TableClient tableClient = new TableClient(
                connectionString,
                NotificationAzureDataTablesConstants.GetTableName(nameof(NotifyMessage)));
            tableClient.CreateIfNotExists();

            // AI: Set the module started flag
            ModuleStarted = true;

            // AI: Start parent module
            applicationBuilder.StartServiceBricksNotification();

            return applicationBuilder;
        }
    }
}