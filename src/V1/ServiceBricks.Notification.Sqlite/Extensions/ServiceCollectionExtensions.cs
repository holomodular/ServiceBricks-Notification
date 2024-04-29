using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Sqlite
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotificationSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationSqliteModule), new NotificationSqliteModule());

            // Add Core service
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            //Register Database
            var builder = new DbContextOptionsBuilder<NotificationSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                NotificationSqliteConstants.APPSETTING_DATABASE_CONNECTION);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
            });
            services.Configure<DbContextOptions<NotificationSqliteContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationSqliteContext>>(builder.Options);
            services.AddDbContext<NotificationSqliteContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<NotifyMessage>, MessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, MessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, MessageStorageRepository>();

            return services;
        }
    }
}