using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceBricks.Storage.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Notification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ServiceBricks.Notification.Sqlite
{
    // dotnet ef migrations add NotificationV1 --context NotificationSqliteContext --startup-project ../Test/MigrationsHost

    /// <summary>
    /// This is the database context for the Notification module.
    /// </summary>
    public partial class NotificationSqliteContext : DbContext, IDesignTimeDbContextFactory<NotificationSqliteContext>
    {
        /// <summary>
        /// Internal.
        /// </summary>
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

        public virtual DbSet<NotifyMessage> NotifyMessage { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Set default schema
            builder.HasDefaultSchema(NotificationSqliteConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<NotifyMessage>().HasKey(key => key.Key);
            builder.Entity<NotifyMessage>().HasIndex(key => new { key.IsComplete, key.IsError, key.IsProcessing, key.SenderTypeKey, key.FutureProcessDate, key.CreateDate });
        }

        /// <summary>
        /// Configure conventions
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTimeOffset>()
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