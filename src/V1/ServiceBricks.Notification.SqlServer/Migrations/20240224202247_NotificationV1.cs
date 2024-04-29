using System;
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
                name: "NotifyMessage",
                schema: "Notification",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderTypeKey = table.Column<int>(type: "int", nullable: false),
                    IsError = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FutureProcessDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
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
                    table.PrimaryKey("PK_NotifyMessage", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotifyMessage_IsComplete_IsError_IsProcessing_SenderTypeKey_FutureProcessDate_CreateDate",
                schema: "Notification",
                table: "NotifyMessage",
                columns: new[] { "IsComplete", "IsError", "IsProcessing", "SenderTypeKey", "FutureProcessDate", "CreateDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotifyMessage",
                schema: "Notification");
        }
    }
}
