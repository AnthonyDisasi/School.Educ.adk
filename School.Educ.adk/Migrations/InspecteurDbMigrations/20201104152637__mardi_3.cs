using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.InspecteurDbMigrations
{
    public partial class _mardi_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lettre",
                table: "Assertions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lettre",
                table: "Assertions",
                nullable: true);
        }
    }
}
