using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBricks.Notification.SqlServer.Migrations
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsError = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    FutureProcessDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProcessDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProcessResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessing = table.Column<bool>(type: "bit", nullable: false),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BccAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CcAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyHtml = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyMessages", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotifyMessages_IsComplete_IsProcessing_IsError_FutureProcessDate_ProcessDate_CreateDate",
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