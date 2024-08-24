using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.SqlServer
{
    // dotnet ef migrations add NotificationV1 --context NotificationSqlServerContext --startup-project ../Tests/MigrationsHost

    /// <summary>
    /// This is the database context for the Notification module.
    /// </summary>
    public partial class NotificationSqlServerContext : DbContext, IDesignTimeDbContextFactory<NotificationSqlServerContext>
    {
        protected readonly DbContextOptions<NotificationSqlServerContext> _options;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationSqlServerContext()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<NotificationSqlServerContext>();
            string connectionString = configuration.GetSqlServerConnectionString(
                NotificationSqlServerConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(NotificationSqlServerContext).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });

            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public NotificationSqlServerContext(DbContextOptions<NotificationSqlServerContext> options) : base(options)
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
            builder.HasDefaultSchema(NotificationSqlServerConstants.DATABASE_SCHEMA_NAME);

            // AI: Setup the entities to the model
            builder.Entity<NotifyMessage>().HasKey(key => key.Key);
            builder.Entity<NotifyMessage>().HasIndex(key => new { key.IsComplete, key.IsProcessing, key.IsError, key.FutureProcessDate, key.ProcessDate, key.CreateDate });
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual NotificationSqlServerContext CreateDbContext(string[] args)
        {
            return new NotificationSqlServerContext(_options);
        }
    }
}