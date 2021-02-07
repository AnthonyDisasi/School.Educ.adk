using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Educ.adk.Migrations.ProfeAreaDbMigrations
{
    public partial class _07_02_2021_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CahierCotes_Cours_CoursID",
                table: "CahierCotes");

            migrationBuilder.DropTable(
                name: "Evaluers");

            migrationBuilder.AlterColumn<string>(
                name: "CoursID",
                table: "CahierCotes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Evaluer",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    LeconID = table.Column<string>(nullable: true),
                    InpecteurID = table.Column<string>(nullable: true),
                    Cotation = table.Column<double>(nullable: false),
                    Remarque = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evaluer_Inspecteur_InpecteurID",
                        column: x => x.InpecteurID,
                        principalTable: "Inspecteur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluer_Lecons_LeconID",
                        column: x => x.LeconID,
                        principalTable: "Lecons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluer_InpecteurID",
                table: "Evaluer",
                column: "InpecteurID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluer_LeconID",
                table: "Evaluer",
                column: "LeconID",
                unique: true,
                filter: "[LeconID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CahierCotes_Cours_CoursID",
                table: "CahierCotes",
                column: "CoursID",
                principalTable: "Cours",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CahierCotes_Cours_CoursID",
                table: "CahierCotes");

            migrationBuilder.DropTable(
                name: "Evaluer");

            migrationBuilder.AlterColumn<string>(
                name: "CoursID",
                table: "CahierCotes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Evaluers",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Cotation = table.Column<double>(nullable: false),
                    InpecteurID = table.Column<string>(nullable: true),
                    LeconID = table.Column<string>(nullable: true),
                    Remarque = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evaluers_Inspecteur_InpecteurID",
                        column: x => x.InpecteurID,
                        principalTable: "Inspecteur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluers_Lecons_LeconID",
                        column: x => x.LeconID,
                        principalTable: "Lecons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluers_InpecteurID",
                table: "Evaluers",
                column: "InpecteurID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluers_LeconID",
                table: "Evaluers",
                column: "LeconID",
                unique: true,
                filter: "[LeconID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CahierCotes_Cours_CoursID",
                table: "CahierCotes",
                column: "CoursID",
                principalTable: "Cours",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
