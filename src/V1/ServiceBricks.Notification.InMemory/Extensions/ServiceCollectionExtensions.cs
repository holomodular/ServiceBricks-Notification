using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.InMemory
{
    /// <summary>
    /// IServiceCollection extensions for the Notification Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksNotificationInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(NotificationInMemoryModule), new NotificationInMemoryModule());

            // Add Core service
            services.AddServiceBricksNotificationEntityFrameworkCore(configuration);

            // Register Database
            var builder = new DbContextOptionsBuilder<NotificationInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString(), b => b.EnableNullChecks(false));
            services.Configure<DbContextOptions<NotificationInMemoryContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<NotificationInMemoryContext>>(builder.Options);
            services.AddDbContext<NotificationInMemoryContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();
            services.AddScoped<INotifyMessageStorageRepository, NotifyMessageStorageRepository>();
            services.AddScoped<IDomainObjectProcessQueueStorageRepository<NotifyMessage>, NotifyMessageStorageRepository>();

            return services;
        }
    }
}