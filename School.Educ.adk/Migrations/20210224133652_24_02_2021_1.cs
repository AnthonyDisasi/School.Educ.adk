using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations
{
    public partial class _24_02_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Affectations");

            migrationBuilder.DropTable(
                name: "Inspecteurs");

            migrationBuilder.DropColumn(
                name: "Lettre",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "DateExamen",
                table: "Participants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lettre",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateExamen",
                table: "Participants",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Inspecteurs",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Genre = table.Column<int>(nullable: true),
                    Matricule = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Postnom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspecteurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Affectations",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DateAffectation = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdEcole = table.Column<string>(nullable: true),
                    InspecteurID = table.Column<string>(nullable: true),
                    PeriodeAffectectation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affectations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Affectations_Inspecteurs_InspecteurID",
                        column: x => x.InspecteurID,
                        principalTable: "Inspecteurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affectations_InspecteurID",
                table: "Affectations",
                column: "InspecteurID",
                unique: true,
                filter: "[InspecteurID] IS NOT NULL");
        }
    }
}
