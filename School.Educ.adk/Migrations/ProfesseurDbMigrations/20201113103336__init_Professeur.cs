using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.ProfesseurDbMigrations
{
    public partial class _init_Professeur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Epreuves",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    IdentifiantCours = table.Column<string>(nullable: false),
                    IdentifiantProfesseur = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    Periode = table.Column<string>(nullable: false),
                    DateEpreuve = table.Column<DateTime>(nullable: false),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epreuves", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lecons",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    IdentifiantProfesseur = table.Column<string>(nullable: true),
                    IdentifiantCours = table.Column<string>(nullable: true),
                    LeconDonnee = table.Column<string>(nullable: true),
                    DateLecon = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cotations",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EpreuveID = table.Column<string>(nullable: true),
                    IdentifiantEleve = table.Column<string>(nullable: false),
                    Point = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cotations_Epreuves_EpreuveID",
                        column: x => x.EpreuveID,
                        principalTable: "Epreuves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Echanges",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    LeconID = table.Column<string>(nullable: true),
                    Inspecteur = table.Column<string>(nullable: true),
                    Cotation = table.Column<double>(nullable: false),
                    Remarque = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Echanges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Echanges_Lecons_LeconID",
                        column: x => x.LeconID,
                        principalTable: "Lecons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotations_EpreuveID",
                table: "Cotations",
                column: "EpreuveID",
                unique: true,
                filter: "[EpreuveID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Echanges_LeconID",
                table: "Echanges",
                column: "LeconID",
                unique: true,
                filter: "[LeconID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotations");

            migrationBuilder.DropTable(
                name: "Echanges");

            migrationBuilder.DropTable(
                name: "Epreuves");

            migrationBuilder.DropTable(
                name: "Lecons");
        }
    }
}
