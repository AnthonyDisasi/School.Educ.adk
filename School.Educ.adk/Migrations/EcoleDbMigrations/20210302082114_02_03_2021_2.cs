using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.EcoleDbMigrations
{
    public partial class _02_03_2021_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "niveau",
                table: "Ecoles",
                newName: "Niveau");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Niveau",
                table: "Ecoles",
                newName: "niveau");
        }
    }
}
