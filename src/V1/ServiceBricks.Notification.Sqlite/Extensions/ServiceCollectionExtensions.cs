using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Sqlite
{
    /// <summary>
    /// Extensions to add the ServiceBricks Notification Sqlite module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Notification Sqlite module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksNotificationSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationSqliteModule), new NotificationSqliteModule());

            // AI: Add parent module
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<NotificationSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                NotificationSqliteConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
            });
            services.Configure<DbContextOptions<NotificationSqliteContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationSqliteContext>>(builder.Options);
            services.AddDbContext<NotificationSqliteContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add storage services for the module. Each domain object should have its own storage repository.
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            return services;
        }
    }
}