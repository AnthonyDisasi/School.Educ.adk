using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _26_01_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfesseurID",
                table: "Cours",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ProfesseurID",
                table: "Cours",
                column: "ProfesseurID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurID",
                table: "Cours",
                column: "ProfesseurID",
                principalTable: "Professeurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurID",
                table: "Cours");

            migrationBuilder.DropIndex(
                name: "IX_Cours_ProfesseurID",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "ProfesseurID",
                table: "Cours");
        }
    }
}
