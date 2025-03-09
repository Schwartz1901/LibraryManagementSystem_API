using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class UserNoti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_ReceiverUserId",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_ReceiverUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "ReceiverUserId",
                table: "Notification",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Notification",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Notification",
                newName: "Message");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notification",
                newName: "ReceiverUserId");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Notification",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Notification",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<Guid>(
                name: "NotificationId",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ReceiverUserId",
                table: "Notification",
                column: "ReceiverUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_ReceiverUserId",
                table: "Notification",
                column: "ReceiverUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
