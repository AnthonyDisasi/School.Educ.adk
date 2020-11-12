using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.ExamenDbMigrations
{
    public partial class _init_Exmanedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examens",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Periode = table.Column<string>(nullable: false),
                    Serie = table.Column<string>(nullable: false),
                    CodeAcces = table.Column<string>(nullable: false),
                    IdInspecteur = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examens", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    IdentifiantEleve = table.Column<string>(nullable: false),
                    DateExamen = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ExamenID = table.Column<string>(nullable: true),
                    Enoncer = table.Column<string>(nullable: true),
                    BonneReponse = table.Column<string>(nullable: true),
                    Cote = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questions_Examens_ExamenID",
                        column: x => x.ExamenID,
                        principalTable: "Examens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assertions",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    QuestionID = table.Column<string>(nullable: false),
                    Intituler = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assertions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assertions_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reponses",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    QuestionID = table.Column<string>(nullable: true),
                    ParticipantID = table.Column<string>(nullable: true),
                    ReponseDonnee = table.Column<string>(nullable: false),
                    Point = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reponses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reponses_Participants_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reponses_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assertions_QuestionID",
                table: "Assertions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamenID",
                table: "Questions",
                column: "ExamenID");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_ParticipantID",
                table: "Reponses",
                column: "ParticipantID");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_QuestionID",
                table: "Reponses",
                column: "QuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assertions");

            migrationBuilder.DropTable(
                name: "Reponses");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Examens");
        }
    }
}
