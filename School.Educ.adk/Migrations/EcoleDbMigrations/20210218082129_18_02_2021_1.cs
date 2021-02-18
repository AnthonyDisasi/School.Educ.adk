using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.EcoleDbMigrations
{
    public partial class _18_02_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluer_Inspecteur_InpecteurID",
                table: "Evaluer");

            migrationBuilder.DropTable(
                name: "Affectation");

            migrationBuilder.DropTable(
                name: "Inspecteur");

            migrationBuilder.CreateTable(
                name: "Inspecteurs",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EcoleID = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspecteurs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inspecteurs_Ecoles_EcoleID",
                        column: x => x.EcoleID,
                        principalTable: "Ecoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inspecteurs_EcoleID",
                table: "Inspecteurs",
                column: "EcoleID",
                unique: true,
                filter: "[EcoleID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluer_Inspecteurs_InpecteurID",
                table: "Evaluer",
                column: "InpecteurID",
                principalTable: "Inspecteurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluer_Inspecteurs_InpecteurID",
                table: "Evaluer");

            migrationBuilder.DropTable(
                name: "Inspecteurs");

            migrationBuilder.CreateTable(
                name: "Inspecteur",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspecteur", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Affectation",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateAffectation = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdEcole = table.Column<string>(nullable: true),
                    InspecteurID = table.Column<string>(nullable: true),
                    PeriodeAffectectation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affectation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Affectation_Inspecteur_InspecteurID",
                        column: x => x.InspecteurID,
                        principalTable: "Inspecteur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_InspecteurID",
                table: "Affectation",
                column: "InspecteurID",
                unique: true,
                filter: "[InspecteurID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluer_Inspecteur_InpecteurID",
                table: "Evaluer",
                column: "InpecteurID",
                principalTable: "Inspecteur",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
