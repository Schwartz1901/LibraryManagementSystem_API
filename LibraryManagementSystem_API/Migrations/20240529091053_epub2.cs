using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class epub2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Epub_EpubId",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "EpubId",
                table: "Book",
                newName: "EpubVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_EpubId",
                table: "Book",
                newName: "IX_Book_EpubVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Epub_EpubVersionId",
                table: "Book",
                column: "EpubVersionId",
                principalTable: "Epub",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Epub_EpubVersionId",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "EpubVersionId",
                table: "Book",
                newName: "EpubId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_EpubVersionId",
                table: "Book",
                newName: "IX_Book_EpubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Epub_EpubId",
                table: "Book",
                column: "EpubId",
                principalTable: "Epub",
                principalColumn: "Id");
        }
    }
}
