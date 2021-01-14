using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.InspecteurDbMigrations
{
    public partial class _12_01_2021_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Affectations_Ecole_EcoleID",
                table: "Affectations");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Inscription");

            migrationBuilder.DropTable(
                name: "Professeur");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropTable(
                name: "Eleve");

            migrationBuilder.DropTable(
                name: "Ecole");

            migrationBuilder.DropTable(
                name: "Directeur");

            migrationBuilder.DropIndex(
                name: "IX_Affectations_EcoleID",
                table: "Affectations");

            migrationBuilder.RenameColumn(
                name: "EcoleID",
                table: "Affectations",
                newName: "InspecteurID");

            migrationBuilder.AddColumn<string>(
                name: "IdEcole",
                table: "Affectations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Affectations_InspecteurID",
                table: "Affectations",
                column: "InspecteurID",
                unique: true,
                filter: "[InspecteurID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Affectations_Inspecteurs_InspecteurID",
                table: "Affectations",
                column: "InspecteurID",
                principalTable: "Inspecteurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Affectations_Inspecteurs_InspecteurID",
                table: "Affectations");

            migrationBuilder.DropIndex(
                name: "IX_Affectations_InspecteurID",
                table: "Affectations");

            migrationBuilder.DropColumn(
                name: "IdEcole",
                table: "Affectations");

            migrationBuilder.RenameColumn(
                name: "InspecteurID",
                table: "Affectations",
                newName: "EcoleID");

            migrationBuilder.CreateTable(
                name: "Directeur",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directeur", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Eleve",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eleve", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ecole",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DirecteurID = table.Column<string>(nullable: true),
                    EcoleLatitude = table.Column<string>(nullable: false),
                    EcoleLongitude = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    SousDivision = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ecole_Directeur_DirecteurID",
                        column: x => x.DirecteurID,
                        principalTable: "Directeur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EcoleID = table.Column<string>(nullable: true),
                    Niveau = table.Column<string>(nullable: false),
                    Option = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Classe_Ecole_EcoleID",
                        column: x => x.EcoleID,
                        principalTable: "Ecole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professeur",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    EcoleID = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professeur", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Professeur_Ecole_EcoleID",
                        column: x => x.EcoleID,
                        principalTable: "Ecole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Categorie = table.Column<string>(nullable: false),
                    ClasseID = table.Column<string>(nullable: true),
                    Intituler = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cours_Classe_ClasseID",
                        column: x => x.ClasseID,
                        principalTable: "Classe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inscription",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    AnneeScolaire = table.Column<string>(nullable: false),
                    ClasseID = table.Column<string>(nullable: true),
                    DateInscription = table.Column<DateTime>(nullable: false),
                    EleveId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscription", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inscription_Classe_ClasseID",
                        column: x => x.ClasseID,
                        principalTable: "Classe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscription_Eleve_EleveId",
                        column: x => x.EleveId,
                        principalTable: "Eleve",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affectations_EcoleID",
                table: "Affectations",
                column: "EcoleID",
                unique: true,
                filter: "[EcoleID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classe_EcoleID",
                table: "Classe",
                column: "EcoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ClasseID",
                table: "Cours",
                column: "ClasseID");

            migrationBuilder.CreateIndex(
                name: "IX_Ecole_DirecteurID",
                table: "Ecole",
                column: "DirecteurID",
                unique: true,
                filter: "[DirecteurID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_ClasseID",
                table: "Inscription",
                column: "ClasseID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_EleveId",
                table: "Inscription",
                column: "EleveId");

            migrationBuilder.CreateIndex(
                name: "IX_Professeur_EcoleID",
                table: "Professeur",
                column: "EcoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Affectations_Ecole_EcoleID",
                table: "Affectations",
                column: "EcoleID",
                principalTable: "Ecole",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
