using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalendarz.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Powiadomienie",
                table: "Kal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Powiadomienie",
                table: "Kal",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
