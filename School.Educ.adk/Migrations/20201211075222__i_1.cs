using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _i_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "liaison",
                table: "Ecoles");

            migrationBuilder.DropColumn(
                name: "liaison",
                table: "Directeurs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
