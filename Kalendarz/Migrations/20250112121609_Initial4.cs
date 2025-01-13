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
            migrationBuilder.DropColumn(
                name: "Udostepnij",
                table: "Kal");

            migrationBuilder.CreateTable(
                name: "Udostepnianie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Udostepnij = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Udostepnianie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Udostepnianie_Kal_KalId",
                        column: x => x.KalId,
                        principalTable: "Kal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Udostepnianie_KalId",
                table: "Udostepnianie",
                column: "KalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Udostepnianie");

            migrationBuilder.AddColumn<bool>(
                name: "Udostepnij",
                table: "Kal",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
