using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.UserAuthentMigrations
{
    public partial class _02_03_2021_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Notifications",
                newName: "DateMessage");

            migrationBuilder.AddColumn<bool>(
                name: "Lu",
                table: "Notifications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lu",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "DateMessage",
                table: "Notifications",
                newName: "MyProperty");
        }
    }
}
