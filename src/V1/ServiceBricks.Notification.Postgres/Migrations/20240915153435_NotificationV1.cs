using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServiceBricks.Notification.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class NotificationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Notification");

            migrationBuilder.CreateTable(
                name: "NotifyMessages",
                schema: "Notification",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderType = table.Column<string>(type: "text", nullable: true),
                    IsError = table.Column<bool>(type: "boolean", nullable: false),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false),
                    RetryCount = table.Column<int>(type: "integer", nullable: false),
                    FutureProcessDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ProcessDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ProcessResponse = table.Column<string>(type: "text", nullable: true),
                    IsProcessing = table.Column<bool>(type: "boolean", nullable: false),
                    IsHtml = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    BccAddress = table.Column<string>(type: "text", nullable: true),
                    CcAddress = table.Column<string>(type: "text", nullable: true),
                    ToAddress = table.Column<string>(type: "text", nullable: true),
                    FromAddress = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    BodyHtml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyMessages", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotifyMessages_IsComplete_IsProcessing_IsError_FutureProces~",
                schema: "Notification",
                table: "NotifyMessages",
                columns: new[] { "IsComplete", "IsProcessing", "IsError", "FutureProcessDate", "ProcessDate", "CreateDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotifyMessages",
                schema: "Notification");
        }
    }
}