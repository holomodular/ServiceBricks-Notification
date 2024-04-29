using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceBricks.Storage.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Notification.EntityFrameworkCore;

namespace ServiceBricks.Notification.InMemory
{
    // dotnet ef migrations add NotificationV1 --context NotificationInMemoryContext --startup-project ../Tests/WebApp

    /// <summary>
    /// This is the database context for the Notification module.
    /// </summary>
    public partial class NotificationInMemoryContext : DbContext, IDesignTimeDbContextFactory<NotificationInMemoryContext>
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly DbContextOptions<NotificationInMemoryContext> _options;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationInMemoryContext()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<NotificationInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public NotificationInMemoryContext(DbContextOptions<NotificationInMemoryContext> options) : base(options)
        {
            _options = options;
        }

        public virtual DbSet<NotifyMessage> NotifyMessage { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Set default schema
            //builder.HasDefaultSchema(NotificationInMemoryConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<NotifyMessage>().HasKey(key => key.Key);
            builder.Entity<NotifyMessage>().HasIndex(key => new { key.IsComplete, key.IsError, key.IsProcessing, key.SenderTypeKey, key.FutureProcessDate, key.CreateDate });
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual NotificationInMemoryContext CreateDbContext(string[] args)
        {
            return new NotificationInMemoryContext(_options);
        }
    }
}