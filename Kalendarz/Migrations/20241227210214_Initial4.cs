using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalendarz.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoIle",
                table: "Kal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Powtarzalnosc",
                table: "Kal",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoIle",
                table: "Kal");

            migrationBuilder.DropColumn(
                name: "Powtarzalnosc",
                table: "Kal");
        }
    }
}
