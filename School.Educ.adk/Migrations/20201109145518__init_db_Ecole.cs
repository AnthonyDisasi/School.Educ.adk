using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _init_db_Ecole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false)
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
                    DateNaissance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directeurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Eleves",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eleves", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.ID);
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
                    Nom = table.Column<string>(nullable: false)
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
                    EcoleLatitude = table.Column<string>(nullable: true),
                    EcoleLongitude = table.Column<string>(nullable: true),
                    SousDivision = table.Column<string>(nullable: false)
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
                name: "Classes",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EcoleID = table.Column<string>(nullable: true),
                    Niveau = table.Column<string>(nullable: false),
                    Section = table.Column<string>(nullable: false),
                    Option = table.Column<string>(nullable: true)
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
                name: "Cours",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ClasseID = table.Column<string>(nullable: true),
                    Intituler = table.Column<string>(nullable: true),
                    Categorie = table.Column<string>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Inscriptions",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EleveId = table.Column<string>(nullable: true),
                    ClasseID = table.Column<string>(nullable: true),
                    DateInscription = table.Column<DateTime>(nullable: false),
                    AnneeScolaire = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Classes_EcoleID",
                table: "Classes",
                column: "EcoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ClasseID",
                table: "Cours",
                column: "ClasseID");

            migrationBuilder.CreateIndex(
                name: "IX_Ecoles_DirecteurID",
                table: "Ecoles",
                column: "DirecteurID",
                unique: true,
                filter: "[DirecteurID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_ClasseID",
                table: "Inscriptions",
                column: "ClasseID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_EleveId",
                table: "Inscriptions",
                column: "EleveId");

            migrationBuilder.CreateIndex(
                name: "IX_Professeurs_EcoleID",
                table: "Professeurs",
                column: "EcoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Inscriptions");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Professeurs");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "SousDivisions");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Eleves");

            migrationBuilder.DropTable(
                name: "Ecoles");

            migrationBuilder.DropTable(
                name: "Directeurs");
        }
    }
}
