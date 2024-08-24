using Microsoft.EntityFrameworkCore;

namespace ServiceBricks.Notification.Cosmos
{
    /// <summary>
    /// This is the database context for the Notification module.
    /// </summary>
    public partial class NotificationCosmosContext : DbContext
    {
        protected readonly DbContextOptions<NotificationCosmosContext> _options;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public NotificationCosmosContext(DbContextOptions<NotificationCosmosContext> options) : base(options)
        {
            _options = options;
        }

        /// <summary>
        /// NotifyMessages.
        /// </summary>
        public virtual DbSet<NotifyMessage> NotifyMessages { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // AI: Create the model for each table
            builder.Entity<NotifyMessage>().HasKey(key => key.Key);
            builder.Entity<NotifyMessage>().HasPartitionKey(key => key.PartitionKey);
            builder.Entity<NotifyMessage>().HasIndex(key => new { key.IsComplete, key.IsProcessing, key.IsError, key.FutureProcessDate, key.ProcessDate, key.CreateDate });
            builder.Entity<NotifyMessage>().ToContainer(NotificationCosmosConstants.GetContainerName(nameof(NotifyMessage)));
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