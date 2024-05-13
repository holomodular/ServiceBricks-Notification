using Azure.Data.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Notification.AzureDataTables
{
    /// <summary>
    /// IApplicationBuilder extensions for the Notification Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksNotificationAzureDataTables(this IApplicationBuilder applicationBuilder)
        {
            var configuration = applicationBuilder.ApplicationServices.GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetAzureDataTablesConnectionString(
                NotificationAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);

            // Create each table if not exists
            TableClient tableClient = new TableClient(
                connectionString,
                NotificationAzureDataTablesConstants.GetTableName(nameof(NotifyMessage)));
            tableClient.CreateIfNotExists();

            ModuleStarted = true;

            // Start core
            applicationBuilder.StartServiceBricksNotification();

            return applicationBuilder;
        }
    }
}