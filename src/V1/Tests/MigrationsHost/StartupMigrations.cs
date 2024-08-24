﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Notification.Postgres;
using ServiceBricks.Notification.Sqlite;
using ServiceBricks.Notification.SqlServer;

namespace ServiceBricks.Xunit
{
    public class StartupMigrations : ServiceBricks.Startup
    {
        public StartupMigrations(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);

            //**************************
            //UNCOMMENT THE ONE YOU NEED
            //**************************
            //services.AddServiceBricksNotificationPostgres(Configuration);
            //services.AddServiceBricksNotificationSqlServer(Configuration);
            services.AddServiceBricksNotificationSqlite(Configuration);

            // Remove all background tasks/timers for unit testing

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();

            //**************************
            //UNCOMMENT THE ONE YOU NEED
            //**************************
            //app.StartServiceBricksNotificationPostgres();
            //app.StartServiceBricksNotificationSqlServer();
            app.StartServiceBricksNotificationSqlite();
        }
    }
}