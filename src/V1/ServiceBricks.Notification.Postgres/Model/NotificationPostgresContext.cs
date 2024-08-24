using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Postgres
{
    // dotnet ef migrations add NotificationV1 --context NotificationPostgresContext --startup-project ../Tests/MigrationsHost

    /// <summary>
    /// This is the database context for the Notification module.
    /// </summary>
    public partial class NotificationPostgresContext : DbContext, IDesignTimeDbContextFactory<NotificationPostgresContext>
    {
        protected readonly DbContextOptions<NotificationPostgresContext> _options;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationPostgresContext()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<NotificationPostgresContext>();
            string connectionString = configuration.GetPostgresConnectionString(
                NotificationPostgresConstants.APPSETTING_CONNECTION_STRING);
            builder.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(NotificationPostgresContext).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });

            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public NotificationPostgresContext(DbContextOptions<NotificationPostgresContext> options) : base(options)
        {
            _options = options;
        }

        /// <summary>
        /// NotifyMessages
        /// </summary>
        public virtual DbSet<NotifyMessage> NotifyMessages { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // AI: Set the default schema
            builder.HasDefaultSchema(NotificationPostgresConstants.DATABASE_SCHEMA_NAME);

            // AI: Setup the entities to the model
            builder.Entity<NotifyMessage>().HasKey(key => key.Key);
            builder.Entity<NotifyMessage>().HasIndex(key => new { key.IsComplete, key.IsProcessing, key.IsError, key.FutureProcessDate, key.ProcessDate, key.CreateDate });
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual NotificationPostgresContext CreateDbContext(string[] args)
        {
            return new NotificationPostgresContext(_options);
        }
    }
}