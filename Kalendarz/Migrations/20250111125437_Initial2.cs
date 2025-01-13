using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalendarz.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoIle",
                table: "Kal");

            migrationBuilder.DropColumn(
                name: "Powtarzalnosc",
                table: "Kal");

            migrationBuilder.CreateTable(
                name: "Powtarzalnosc",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Powtorz = table.Column<bool>(type: "bit", nullable: false),
                    CoIle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrzezIle = table.Column<int>(type: "int", nullable: false),
                    KalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powtarzalnosc", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Powtarzalnosc_Kal_KalId",
                        column: x => x.KalId,
                        principalTable: "Kal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Powtarzalnosc_KalId",
                table: "Powtarzalnosc",
                column: "KalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Powtarzalnosc");

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
    }
}
