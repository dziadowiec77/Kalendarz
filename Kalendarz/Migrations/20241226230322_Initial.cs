using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalendarz.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "AspNetRole",
                schema: "Security",
                columns: table => new
                {
                    AspNetRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRole", x => x.AspNetRoleId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUser",
                schema: "Security",
                columns: table => new
                {
                    AspNetUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUser", x => x.AspNetUserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaim",
                schema: "Security",
                columns: table => new
                {
                    AspNetRoleClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetRoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaim", x => x.AspNetRoleClaimId);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaim_AspNetRole_AspNetRoleId",
                        column: x => x.AspNetRoleId,
                        principalSchema: "Security",
                        principalTable: "AspNetRole",
                        principalColumn: "AspNetRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaim",
                schema: "Security",
                columns: table => new
                {
                    AspNetUserClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetUserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaim", x => x.AspNetUserClaimId);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaim_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "Security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogin",
                schema: "Security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspNetUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogin_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "Security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRole",
                schema: "Security",
                columns: table => new
                {
                    AspNetUserId = table.Column<int>(type: "int", nullable: false),
                    AspNetRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRole", x => new { x.AspNetUserId, x.AspNetRoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRole_AspNetRole_AspNetRoleId",
                        column: x => x.AspNetRoleId,
                        principalSchema: "Security",
                        principalTable: "AspNetRole",
                        principalColumn: "AspNetRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRole_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "Security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserToken",
                schema: "Security",
                columns: table => new
                {
                    AspNetUserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserToken", x => new { x.AspNetUserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserToken_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "Security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypWydarzenia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kolor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypWydarzenia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TypWydarzenia_AspNetUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KalendarzUserId = table.Column<int>(type: "int", nullable: false),
                    TypWydarzeniaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                table.PrimaryKey("PK_Kal", x => x.ID);
                table.ForeignKey(
                    name: "FK_Kal_AspNetUser_KalendarzUserId",
                    column: x => x.KalendarzUserId,
                    principalSchema: "Security",
                    principalTable: "AspNetUser",
                    principalColumn: "AspNetUserId",
                    onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kal_TypWydarzenia_TypWydarzeniaId",
                        column: x => x.TypWydarzeniaId,
                        principalTable: "TypWydarzenia",
                        principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "AspNetRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaim_AspNetRoleId",
                schema: "Security",
                table: "AspNetRoleClaim",
                column: "AspNetRoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "AspNetUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "AspNetUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaim_AspNetUserId",
                schema: "Security",
                table: "AspNetUserClaim",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogin_AspNetUserId",
                schema: "Security",
                table: "AspNetUserLogin",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRole_AspNetRoleId",
                schema: "Security",
                table: "AspNetUserRole",
                column: "AspNetRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Kal_KalendarzUserId",
                table: "Kal",
                column: "KalendarzUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Kal_TypWydarzeniaId",
                table: "Kal",
                column: "TypWydarzeniaId");

            migrationBuilder.CreateIndex(
                name: "IX_TypWydarzenia_UserId",
                table: "TypWydarzenia",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "AspNetUserClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "AspNetUserLogin",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "AspNetUserRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "AspNetUserToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Kal");

            migrationBuilder.DropTable(
                name: "AspNetRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "TypWydarzenia");

            migrationBuilder.DropTable(
                name: "AspNetUser",
                schema: "Security");
        }
    }
}
