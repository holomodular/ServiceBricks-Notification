using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBricks.Notification.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class NotificationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotifyMessages",
                columns: table => new
                {
                    Key = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SenderType = table.Column<string>(type: "TEXT", nullable: true),
                    IsError = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsComplete = table.Column<bool>(type: "INTEGER", nullable: false),
                    RetryCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FutureProcessDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UpdateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ProcessDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ProcessResponse = table.Column<string>(type: "TEXT", nullable: true),
                    IsProcessing = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsHtml = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", nullable: true),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    BccAddress = table.Column<string>(type: "TEXT", nullable: true),
                    CcAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ToAddress = table.Column<string>(type: "TEXT", nullable: true),
                    FromAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    BodyHtml = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyMessages", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotifyMessages_IsComplete_IsProcessing_IsError_FutureProcessDate_ProcessDate_CreateDate",
                table: "NotifyMessages",
                columns: new[] { "IsComplete", "IsProcessing", "IsError", "FutureProcessDate", "ProcessDate", "CreateDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotifyMessages");
        }
    }
}
