using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Postgres
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotificationPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationPostgresModule), new NotificationPostgresModule());

            // Add Core service
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            //Register Database
            var builder = new DbContextOptionsBuilder<NotificationPostgresContext>();
            string connectionString = configuration.GetPostgresConnectionString(
                NotificationPostgresConstants.APPSETTING_CONNECTION_STRING);
            builder.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<NotificationPostgresContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationPostgresContext>>(builder.Options);
            services.AddDbContext<NotificationPostgresContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<NotifyMessage>, MessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, MessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, MessageStorageRepository>();

            return services;
        }
    }
}