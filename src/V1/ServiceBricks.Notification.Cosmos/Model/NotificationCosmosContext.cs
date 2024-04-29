using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceBricks.Storage.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is the database context for the Notification module.
    /// </summary>
    public partial class NotificationCosmosContext : DbContext
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly DbContextOptions<NotificationCosmosContext> _options;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public NotificationCosmosContext(DbContextOptions<NotificationCosmosContext> options) : base(options)
        {
            _options = options;
        }

        public virtual DbSet<NotifyMessage> NotifyMessages { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Model.SetDefaultContainer(NotificationCosmosConstants.DEFAULT_CONTAINER_NAME);

            builder.Entity<NotifyMessage>().HasKey(key => key.Key);
            builder.Entity<NotifyMessage>().HasIndex(key => new { key.IsComplete, key.IsError, key.IsProcessing, key.SenderTypeKey, key.FutureProcessDate, key.CreateDate });
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual NotificationCosmosContext CreateDbContext(string[] args)
        {
            return new NotificationCosmosContext(_options);
        }
    }
}