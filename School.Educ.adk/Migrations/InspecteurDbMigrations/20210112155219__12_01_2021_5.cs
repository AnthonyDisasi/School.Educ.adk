using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.InspecteurDbMigrations
{
    public partial class _12_01_2021_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Affectations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodeAffectectation",
                table: "Affectations",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Affectations");

            migrationBuilder.DropColumn(
                name: "PeriodeAffectectation",
                table: "Affectations");
        }
    }
}
