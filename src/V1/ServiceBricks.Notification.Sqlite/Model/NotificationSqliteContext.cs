using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Notification.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Notification.Sqlite
{
    // dotnet ef migrations add NotificationV1 --context NotificationSqliteContext --startup-project ../Tests/MigrationsHost

    /// <summary>
    /// This is the database context for the Notification module.
    /// </summary>
    public partial class NotificationSqliteContext : DbContext, IDesignTimeDbContextFactory<NotificationSqliteContext>
    {
        protected readonly DbContextOptions<NotificationSqliteContext> _options;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationSqliteContext()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<NotificationSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                NotificationSqliteConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(NotificationSqliteContext).Assembly.GetName().Name);
            });
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public NotificationSqliteContext(DbContextOptions<NotificationSqliteContext> options) : base(options)
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
            //builder.HasDefaultSchema(NotificationSqliteConstants.DATABASE_SCHEMA_NAME);

            // AI: Setup the entities to the model
            builder.Entity<NotifyMessage>().HasKey(key => key.Key);
            builder.Entity<NotifyMessage>().HasIndex(key => new { key.IsComplete, key.IsProcessing, key.IsError, key.FutureProcessDate, key.ProcessDate, key.CreateDate });
        }

        /// <summary>
        /// Configure conventions
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<DateTimeOffset>()
                .HaveConversion<DateTimeOffsetToBytesConverter>();
            configurationBuilder
                .Properties<DateTimeOffset?>()
                .HaveConversion<DateTimeOffsetToBytesConverter>();
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual NotificationSqliteContext CreateDbContext(string[] args)
        {
            return new NotificationSqliteContext(_options);
        }
    }
}