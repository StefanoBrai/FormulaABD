using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaABD.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piloti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piloti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracciati",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracciati", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Risultati",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TracciatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PilotaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TempoGiro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posizione = table.Column<int>(type: "int", nullable: false),
                    PunteggioPosizione = table.Column<int>(type: "int", nullable: false),
                    PunteggioDistacco = table.Column<int>(type: "int", nullable: false),
                    TotalePunteggioGara = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risultati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risultati_Piloti_PilotaId",
                        column: x => x.PilotaId,
                        principalTable: "Piloti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Risultati_PilotaId",
                table: "Risultati",
                column: "PilotaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Risultati");

            migrationBuilder.DropTable(
                name: "Tracciati");

            migrationBuilder.DropTable(
                name: "Piloti");
        }
    }
}
