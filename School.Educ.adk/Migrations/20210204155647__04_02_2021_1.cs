using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _04_02_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurID",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "AnneeScolaire",
                table: "Inscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "ProfesseurID",
                table: "Cours",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnneeScolaire",
                table: "Classes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "categories",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurID",
                table: "Cours",
                column: "ProfesseurID",
                principalTable: "Professeurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurID",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "AnneeScolaire",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "AnneeScolaire",
                table: "Inscriptions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProfesseurID",
                table: "Cours",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "categories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurID",
                table: "Cours",
                column: "ProfesseurID",
                principalTable: "Professeurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
