using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class epub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpubId",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Epub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epub", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_EpubId",
                table: "Book",
                column: "EpubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Epub_EpubId",
                table: "Book",
                column: "EpubId",
                principalTable: "Epub",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Epub_EpubId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Epub");

            migrationBuilder.DropIndex(
                name: "IX_Book_EpubId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "EpubId",
                table: "Book");
        }
    }
}
