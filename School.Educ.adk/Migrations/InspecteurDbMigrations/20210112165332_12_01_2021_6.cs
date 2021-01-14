using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.InspecteurDbMigrations
{
    public partial class _12_01_2021_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Affectations_Inspecteurs_InspecteurID",
                table: "Affectations");

            migrationBuilder.DropIndex(
                name: "IX_Affectations_InspecteurID",
                table: "Affectations");

            migrationBuilder.DropColumn(
                name: "InspecteurID",
                table: "Affectations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InspecteurID",
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
    }
}
