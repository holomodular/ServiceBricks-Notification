using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.SqlServer
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotificationSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationSqlServerModule), new NotificationSqlServerModule());

            // Add Core service
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            //Register Database
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

            // Storage Services
            services.AddScoped<IStorageRepository<NotifyMessage>, MessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, MessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, MessageStorageRepository>();

            return services;
        }
    }
}