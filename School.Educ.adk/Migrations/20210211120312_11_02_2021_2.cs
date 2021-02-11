using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _11_02_2021_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CahierCote",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    CoursID = table.Column<string>(nullable: false),
                    Periode = table.Column<string>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CahierCote", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CahierCote_Cours_CoursID",
                        column: x => x.CoursID,
                        principalTable: "Cours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecons",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ProfesseurID = table.Column<string>(nullable: true),
                    CoursID = table.Column<string>(nullable: true),
                    LeconDonnee = table.Column<string>(nullable: true),
                    DateLecon = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lecons_Cours_CoursID",
                        column: x => x.CoursID,
                        principalTable: "Cours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lecons_Professeurs_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Epreuve",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    CahierCoteID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    DateEpreuve = table.Column<DateTime>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epreuve", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Epreuve_CahierCote_CahierCoteID",
                        column: x => x.CahierCoteID,
                        principalTable: "CahierCote",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cotations",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EpreuveID = table.Column<string>(nullable: true),
                    EleveID = table.Column<string>(nullable: false),
                    Point = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cotations_Eleves_EleveID",
                        column: x => x.EleveID,
                        principalTable: "Eleves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cotations_Epreuve_EpreuveID",
                        column: x => x.EpreuveID,
                        principalTable: "Epreuve",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CahierCote_CoursID",
                table: "CahierCote",
                column: "CoursID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotations_EleveID",
                table: "Cotations",
                column: "EleveID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotations_EpreuveID",
                table: "Cotations",
                column: "EpreuveID");

            migrationBuilder.CreateIndex(
                name: "IX_Epreuve_CahierCoteID",
                table: "Epreuve",
                column: "CahierCoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Lecons_CoursID",
                table: "Lecons",
                column: "CoursID");

            migrationBuilder.CreateIndex(
                name: "IX_Lecons_ProfesseurID",
                table: "Lecons",
                column: "ProfesseurID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotations");

            migrationBuilder.DropTable(
                name: "Lecons");

            migrationBuilder.DropTable(
                name: "Epreuve");

            migrationBuilder.DropTable(
                name: "CahierCote");
        }
    }
}
