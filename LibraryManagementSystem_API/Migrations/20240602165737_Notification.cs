using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class Notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Notification",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "IsSent",
                table: "Notification",
                newName: "Read");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Notification",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Read",
                table: "Notification",
                newName: "IsSent");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
