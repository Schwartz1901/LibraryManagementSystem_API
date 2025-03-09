using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem_API.Migrations
{
    /// <inheritdoc />
    public partial class rating2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Comment",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
