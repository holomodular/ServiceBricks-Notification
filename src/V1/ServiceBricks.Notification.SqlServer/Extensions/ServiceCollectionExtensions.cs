using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.SqlServer
{
    /// <summary>
    /// Extension methods for configuring the ServiceBricks Notification SqlServer module.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ServiceBricks Notification SqlServer module to the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationSqlServerModule), new NotificationSqlServerModule());

            // AI: Add parent module
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<NotificationSqlServerContext>();
            string connectionString = configuration.GetSqlServerConnectionString(
                NotificationSqlServerConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<NotificationSqlServerContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationSqlServerContext>>(builder.Options);
            services.AddDbContext<NotificationSqlServerContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add storage services for the module. Each domain object should have its own storage repository.
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            return services;
        }
    }
}