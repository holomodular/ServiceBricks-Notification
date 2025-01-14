using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        public virtual DbSet<NotifyMessage> NotifyMessage { get; set; }

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
            builder.Entity<NotifyMessage>().ToContainer(NotificationCosmosConstants.GetContainerName(nameof(NotifyMessage)));
        }

        /// <summary>
        /// OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if NET9_0
            optionsBuilder.ConfigureWarnings(w => w.Ignore(CosmosEventId.SyncNotSupported));
#endif

            base.OnConfiguring(optionsBuilder);
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