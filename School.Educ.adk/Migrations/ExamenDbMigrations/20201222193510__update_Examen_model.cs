using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.ExamenDbMigrations
{
    public partial class _update_Examen_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Examens_ExamenID",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reponses_Participants_ParticipantID",
                table: "Reponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reponses_Questions_QuestionID",
                table: "Reponses");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionID",
                table: "Reponses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantID",
                table: "Reponses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExamenID",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Serie",
                table: "Examens",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Lecon",
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
                    table.PrimaryKey("PK_Lecon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Echange",
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
                    table.PrimaryKey("PK_Echange", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Echange_Lecon_LeconID",
                        column: x => x.LeconID,
                        principalTable: "Lecon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Echange_LeconID",
                table: "Echange",
                column: "LeconID",
                unique: true,
                filter: "[LeconID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Examens_ExamenID",
                table: "Questions",
                column: "ExamenID",
                principalTable: "Examens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reponses_Participants_ParticipantID",
                table: "Reponses",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reponses_Questions_QuestionID",
                table: "Reponses",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Examens_ExamenID",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reponses_Participants_ParticipantID",
                table: "Reponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reponses_Questions_QuestionID",
                table: "Reponses");

            migrationBuilder.DropTable(
                name: "Echange");

            migrationBuilder.DropTable(
                name: "Lecon");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionID",
                table: "Reponses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantID",
                table: "Reponses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ExamenID",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Serie",
                table: "Examens",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Examens_ExamenID",
                table: "Questions",
                column: "ExamenID",
                principalTable: "Examens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reponses_Participants_ParticipantID",
                table: "Reponses",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reponses_Questions_QuestionID",
                table: "Reponses",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
