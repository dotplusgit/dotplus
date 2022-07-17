using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class DivisitionAddedtoPatientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "District",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DivisionId",
                table: "Patient",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_District_DivisionId",
                table: "District",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Division_DivisionId",
                table: "District",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Division_DivisionId",
                table: "Patient",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_Division_DivisionId",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Division_DivisionId",
                table: "Patient");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropIndex(
                name: "IX_Patient_DivisionId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_District_DivisionId",
                table: "District");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "District");
        }
    }
}
