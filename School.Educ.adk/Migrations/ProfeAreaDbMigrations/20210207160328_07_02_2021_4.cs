using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.ProfeAreaDbMigrations
{
    public partial class _07_02_2021_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directeur",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directeur", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Inspecteur",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_Inspecteur", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ecole",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DirecteurID = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: false),
                    EcoleLatitude = table.Column<string>(nullable: false),
                    EcoleLongitude = table.Column<string>(nullable: false),
                    SousDivision = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false)
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
                name: "Affectation",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    InspecteurID = table.Column<string>(nullable: true),
                    IdEcole = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PeriodeAffectectation = table.Column<string>(nullable: false),
                    DateAffectation = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EcoleID = table.Column<string>(nullable: true),
                    Niveau = table.Column<string>(nullable: false),
                    Section = table.Column<string>(nullable: false),
                    AnneeScolaire = table.Column<string>(nullable: false)
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
                name: "Eleve",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EcoleID = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eleve", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Eleve_Ecole_EcoleID",
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
                    EcoleID = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false)
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
                name: "Inscription",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EleveId = table.Column<string>(nullable: true),
                    ClasseID = table.Column<string>(nullable: true),
                    DateInscription = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ClasseID = table.Column<string>(nullable: true),
                    ProfesseurID = table.Column<string>(nullable: false),
                    Intituler = table.Column<string>(nullable: false),
                    Categorie = table.Column<string>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Cours_Professeur_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CahierCotes",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    CoursID = table.Column<string>(nullable: true),
                    Periode = table.Column<string>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CahierCotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CahierCotes_Cours_CoursID",
                        column: x => x.CoursID,
                        principalTable: "Cours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_Lecons_Professeur_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Epreuves",
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
                    table.PrimaryKey("PK_Epreuves", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Epreuves_CahierCotes_CahierCoteID",
                        column: x => x.CahierCoteID,
                        principalTable: "CahierCotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluers",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    LeconID = table.Column<string>(nullable: true),
                    InpecteurID = table.Column<string>(nullable: true),
                    Cotation = table.Column<double>(nullable: false),
                    Remarque = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evaluers_Inspecteur_InpecteurID",
                        column: x => x.InpecteurID,
                        principalTable: "Inspecteur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluers_Lecons_LeconID",
                        column: x => x.LeconID,
                        principalTable: "Lecons",
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
                        name: "FK_Cotations_Eleve_EleveID",
                        column: x => x.EleveID,
                        principalTable: "Eleve",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cotations_Epreuves_EpreuveID",
                        column: x => x.EpreuveID,
                        principalTable: "Epreuves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_InspecteurID",
                table: "Affectation",
                column: "InspecteurID",
                unique: true,
                filter: "[InspecteurID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CahierCotes_CoursID",
                table: "CahierCotes",
                column: "CoursID");

            migrationBuilder.CreateIndex(
                name: "IX_Classe_EcoleID",
                table: "Classe",
                column: "EcoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotations_EleveID",
                table: "Cotations",
                column: "EleveID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotations_EpreuveID",
                table: "Cotations",
                column: "EpreuveID");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ClasseID",
                table: "Cours",
                column: "ClasseID");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ProfesseurID",
                table: "Cours",
                column: "ProfesseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Ecole_DirecteurID",
                table: "Ecole",
                column: "DirecteurID",
                unique: true,
                filter: "[DirecteurID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Eleve_EcoleID",
                table: "Eleve",
                column: "EcoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Epreuves_CahierCoteID",
                table: "Epreuves",
                column: "CahierCoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluers_InpecteurID",
                table: "Evaluers",
                column: "InpecteurID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluers_LeconID",
                table: "Evaluers",
                column: "LeconID",
                unique: true,
                filter: "[LeconID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_ClasseID",
                table: "Inscription",
                column: "ClasseID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_EleveId",
                table: "Inscription",
                column: "EleveId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecons_CoursID",
                table: "Lecons",
                column: "CoursID");

            migrationBuilder.CreateIndex(
                name: "IX_Lecons_ProfesseurID",
                table: "Lecons",
                column: "ProfesseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Professeur_EcoleID",
                table: "Professeur",
                column: "EcoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Affectation");

            migrationBuilder.DropTable(
                name: "Cotations");

            migrationBuilder.DropTable(
                name: "Evaluers");

            migrationBuilder.DropTable(
                name: "Inscription");

            migrationBuilder.DropTable(
                name: "Epreuves");

            migrationBuilder.DropTable(
                name: "Inspecteur");

            migrationBuilder.DropTable(
                name: "Lecons");

            migrationBuilder.DropTable(
                name: "Eleve");

            migrationBuilder.DropTable(
                name: "CahierCotes");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropTable(
                name: "Professeur");

            migrationBuilder.DropTable(
                name: "Ecole");

            migrationBuilder.DropTable(
                name: "Directeur");
        }
    }
}
