using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _12_02_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CahierCote_CoursID",
                table: "CahierCote");

            migrationBuilder.CreateIndex(
                name: "IX_CahierCote_CoursID",
                table: "CahierCote",
                column: "CoursID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CahierCote_CoursID",
                table: "CahierCote");

            migrationBuilder.CreateIndex(
                name: "IX_CahierCote_CoursID",
                table: "CahierCote",
                column: "CoursID");
        }
    }
}
