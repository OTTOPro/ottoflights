using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OttoFlights.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vol",
                columns: table => new
                {
                    VolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DatePrevue = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateRevue = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumeroVol = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Agence = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Provenance = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Etat = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vol", x => x.VolId);
                });

            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    EvenementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateRevisée = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Statut = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement", x => x.EvenementId);
                    table.ForeignKey(
                        name: "FK_Evenement_Vol_VolId",
                        column: x => x.VolId,
                        principalTable: "Vol",
                        principalColumn: "VolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_VolId",
                table: "Evenement",
                column: "VolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Vol");
        }
    }
}
