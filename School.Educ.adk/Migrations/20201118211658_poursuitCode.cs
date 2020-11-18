using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class poursuitCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Professeurs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AnneeScolaire",
                table: "Inscriptions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Eleves",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EcoleLongitude",
                table: "Ecoles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EcoleLatitude",
                table: "Ecoles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Directeurs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Intituler",
                table: "Cours",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Categorie",
                table: "Cours",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Examen",
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
                    table.PrimaryKey("PK_Examen", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    IdentifiantEleve = table.Column<string>(nullable: false),
                    DateExamen = table.Column<DateTime>(nullable: false)
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
                    ExamenID = table.Column<string>(nullable: false),
                    Enoncer = table.Column<string>(nullable: true),
                    BonneReponse = table.Column<string>(nullable: true),
                    Cote = table.Column<double>(nullable: false)
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
                    QuestionID = table.Column<string>(nullable: false),
                    Intituler = table.Column<string>(nullable: false)
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
                    QuestionID = table.Column<string>(nullable: false),
                    ParticipantID = table.Column<string>(nullable: false),
                    ReponseDonnee = table.Column<string>(nullable: false),
                    Point = table.Column<double>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Professeurs");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Eleves");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Directeurs");

            migrationBuilder.AlterColumn<string>(
                name: "AnneeScolaire",
                table: "Inscriptions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EcoleLongitude",
                table: "Ecoles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EcoleLatitude",
                table: "Ecoles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Intituler",
                table: "Cours",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Categorie",
                table: "Cours",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
