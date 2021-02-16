using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _16_02_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecons_Cours_CoursID",
                table: "Lecons");

            migrationBuilder.AlterColumn<string>(
                name: "LeconDonnee",
                table: "Lecons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoursID",
                table: "Lecons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecons_Cours_CoursID",
                table: "Lecons",
                column: "CoursID",
                principalTable: "Cours",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecons_Cours_CoursID",
                table: "Lecons");

            migrationBuilder.AlterColumn<string>(
                name: "LeconDonnee",
                table: "Lecons",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CoursID",
                table: "Lecons",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Lecons_Cours_CoursID",
                table: "Lecons",
                column: "CoursID",
                principalTable: "Cours",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
