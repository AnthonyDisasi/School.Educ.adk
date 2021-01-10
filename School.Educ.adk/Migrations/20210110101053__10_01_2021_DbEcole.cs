using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _10_01_2021_DbEcole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Periode",
                table: "Examen",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateExamen",
                table: "Examen",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Duree",
                table: "Examen",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DateCreate",
                table: "Ecoles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExamen",
                table: "Examen");

            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Examen");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Ecoles");

            migrationBuilder.AlterColumn<string>(
                name: "Periode",
                table: "Examen",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
