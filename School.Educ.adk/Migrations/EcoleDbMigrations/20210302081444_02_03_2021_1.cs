using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.EcoleDbMigrations
{
    public partial class _02_03_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "niveau",
                table: "Ecoles",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "niveau",
                table: "Ecoles");
        }
    }
}
