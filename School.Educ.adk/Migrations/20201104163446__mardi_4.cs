using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _mardi_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SectionID",
                table: "Options",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_SectionID",
                table: "Options",
                column: "SectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Sections_SectionID",
                table: "Options",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Sections_SectionID",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_SectionID",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "Options");
        }
    }
}
