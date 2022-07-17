using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class DistrictAndUpazilaAddedToDbPatientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Upazila",
                table: "Patient");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpazilaId",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Upazila",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upazila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Upazila_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DistrictId",
                table: "Patient",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UpazilaId",
                table: "Patient",
                column: "UpazilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Upazila_DistrictId",
                table: "Upazila",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_District_DistrictId",
                table: "Patient",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Upazila_UpazilaId",
                table: "Patient",
                column: "UpazilaId",
                principalTable: "Upazila",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_District_DistrictId",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Upazila_UpazilaId",
                table: "Patient");

            migrationBuilder.DropTable(
                name: "Upazila");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropIndex(
                name: "IX_Patient_DistrictId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_UpazilaId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "UpazilaId",
                table: "Patient");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Upazila",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
