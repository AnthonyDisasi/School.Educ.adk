using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _initi_92 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "liaison",
                table: "Ecoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "liaison",
                table: "Directeurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "liaison",
                table: "Ecoles");

            migrationBuilder.DropColumn(
                name: "liaison",
                table: "Directeurs");
        }
    }
}
