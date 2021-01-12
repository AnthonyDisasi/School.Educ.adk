using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _12_01_2021_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assertion");

            migrationBuilder.DropTable(
                name: "Reponse");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Examen");

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
                name: "Affectation",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EcoleID = table.Column<string>(nullable: true),
                    InspecteurID = table.Column<string>(nullable: true),
                    DateAffectation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affectation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Affectation_Ecoles_EcoleID",
                        column: x => x.EcoleID,
                        principalTable: "Ecoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Affectation_Inspecteur_InspecteurID",
                        column: x => x.InspecteurID,
                        principalTable: "Inspecteur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_EcoleID",
                table: "Affectation",
                column: "EcoleID",
                unique: true,
                filter: "[EcoleID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_InspecteurID",
                table: "Affectation",
                column: "InspecteurID",
                unique: true,
                filter: "[InspecteurID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Affectation");

            migrationBuilder.DropTable(
                name: "Inspecteur");

            migrationBuilder.CreateTable(
                name: "Examen",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    CodeAcces = table.Column<string>(nullable: false),
                    DateExamen = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Duree = table.Column<DateTime>(nullable: false),
                    IdInspecteur = table.Column<string>(nullable: false),
                    Periode = table.Column<int>(nullable: false),
                    Serie = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examen", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateExamen = table.Column<DateTime>(nullable: false),
                    IdentifiantEleve = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    BonneReponse = table.Column<string>(nullable: true),
                    Cote = table.Column<double>(nullable: false),
                    Enoncer = table.Column<string>(nullable: true),
                    ExamenID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Question_Examen_ExamenID",
                        column: x => x.ExamenID,
                        principalTable: "Examen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assertion",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Intituler = table.Column<string>(nullable: false),
                    QuestionID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assertion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assertion_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reponse",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ParticipantID = table.Column<string>(nullable: false),
                    Point = table.Column<double>(nullable: false),
                    QuestionID = table.Column<string>(nullable: false),
                    ReponseDonnee = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reponse", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reponse_Participant_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reponse_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assertion_QuestionID",
                table: "Assertion",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ExamenID",
                table: "Question",
                column: "ExamenID");

            migrationBuilder.CreateIndex(
                name: "IX_Reponse_ParticipantID",
                table: "Reponse",
                column: "ParticipantID");

            migrationBuilder.CreateIndex(
                name: "IX_Reponse_QuestionID",
                table: "Reponse",
                column: "QuestionID");
        }
    }
}
