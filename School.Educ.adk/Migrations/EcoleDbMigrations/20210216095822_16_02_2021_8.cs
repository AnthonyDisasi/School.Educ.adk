using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.EcoleDbMigrations
{
    public partial class _16_02_2021_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Directeurs",
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
                    table.PrimaryKey("PK_Directeurs", x => x.ID);
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
                name: "Sections",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SousDivisions",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    LocalDescript = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousDivisions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ecoles",
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
                    table.PrimaryKey("PK_Ecoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ecoles_Directeurs_DirecteurID",
                        column: x => x.DirecteurID,
                        principalTable: "Directeurs",
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
                name: "Options",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    SectionID = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Options_Sections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
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
                    table.PrimaryKey("PK_Classes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Classes_Ecoles_EcoleID",
                        column: x => x.EcoleID,
                        principalTable: "Ecoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eleves",
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
                    table.PrimaryKey("PK_Eleves", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Eleves_Ecoles_EcoleID",
                        column: x => x.EcoleID,
                        principalTable: "Ecoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professeurs",
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
                    table.PrimaryKey("PK_Professeurs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Professeurs_Ecoles_EcoleID",
                        column: x => x.EcoleID,
                        principalTable: "Ecoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inscriptions",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EleveId = table.Column<string>(nullable: true),
                    ClasseID = table.Column<string>(nullable: true),
                    DateInscription = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscriptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Classes_ClasseID",
                        column: x => x.ClasseID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Eleves_EleveId",
                        column: x => x.EleveId,
                        principalTable: "Eleves",
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
                        name: "FK_Cours_Classes_ClasseID",
                        column: x => x.ClasseID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cours_Professeurs_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    CoursID = table.Column<string>(nullable: false),
                    LeconDonnee = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "Evaluer",
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
                    table.PrimaryKey("PK_Evaluer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evaluer_Inspecteur_InpecteurID",
                        column: x => x.InpecteurID,
                        principalTable: "Inspecteur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluer_Lecons_LeconID",
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
                name: "IX_Affectation_InspecteurID",
                table: "Affectation",
                column: "InspecteurID",
                unique: true,
                filter: "[InspecteurID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CahierCote_CoursID",
                table: "CahierCote",
                column: "CoursID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_EcoleID",
                table: "Classes",
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
                name: "IX_Ecoles_DirecteurID",
                table: "Ecoles",
                column: "DirecteurID",
                unique: true,
                filter: "[DirecteurID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_EcoleID",
                table: "Eleves",
                column: "EcoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Epreuve_CahierCoteID",
                table: "Epreuve",
                column: "CahierCoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluer_InpecteurID",
                table: "Evaluer",
                column: "InpecteurID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluer_LeconID",
                table: "Evaluer",
                column: "LeconID",
                unique: true,
                filter: "[LeconID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_ClasseID",
                table: "Inscriptions",
                column: "ClasseID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_EleveId",
                table: "Inscriptions",
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
                name: "IX_Options_SectionID",
                table: "Options",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Professeurs_EcoleID",
                table: "Professeurs",
                column: "EcoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Affectation");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "Cotations");

            migrationBuilder.DropTable(
                name: "Evaluer");

            migrationBuilder.DropTable(
                name: "Inscriptions");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "SousDivisions");

            migrationBuilder.DropTable(
                name: "Epreuve");

            migrationBuilder.DropTable(
                name: "Inspecteur");

            migrationBuilder.DropTable(
                name: "Lecons");

            migrationBuilder.DropTable(
                name: "Eleves");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "CahierCote");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Professeurs");

            migrationBuilder.DropTable(
                name: "Ecoles");

            migrationBuilder.DropTable(
                name: "Directeurs");
        }
    }
}
