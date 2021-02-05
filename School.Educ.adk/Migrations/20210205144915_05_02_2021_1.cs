using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _05_02_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EcoleID",
                table: "Eleves",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_EcoleID",
                table: "Eleves",
                column: "EcoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Eleves_Ecoles_EcoleID",
                table: "Eleves",
                column: "EcoleID",
                principalTable: "Ecoles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eleves_Ecoles_EcoleID",
                table: "Eleves");

            migrationBuilder.DropIndex(
                name: "IX_Eleves_EcoleID",
                table: "Eleves");

            migrationBuilder.DropColumn(
                name: "EcoleID",
                table: "Eleves");
        }
    }
}
