using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class divDisUpaAddedToHospitalandBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "Upazilla",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Upazila",
                table: "Branch");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpazilaId",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Branch",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "Branch",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpazilaId",
                table: "Branch",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_DistrictId",
                table: "Hospital",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_DivisionId",
                table: "Hospital",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_UpazilaId",
                table: "Hospital",
                column: "UpazilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_DistrictId",
                table: "Branch",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_DivisionId",
                table: "Branch",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_UpazilaId",
                table: "Branch",
                column: "UpazilaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_District_DistrictId",
                table: "Branch",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Division_DivisionId",
                table: "Branch",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Upazila_UpazilaId",
                table: "Branch",
                column: "UpazilaId",
                principalTable: "Upazila",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_District_DistrictId",
                table: "Hospital",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_Division_DivisionId",
                table: "Hospital",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_Upazila_UpazilaId",
                table: "Hospital",
                column: "UpazilaId",
                principalTable: "Upazila",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_District_DistrictId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Division_DivisionId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Upazila_UpazilaId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_District_DistrictId",
                table: "Hospital");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_Division_DivisionId",
                table: "Hospital");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_Upazila_UpazilaId",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Hospital_DistrictId",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Hospital_DivisionId",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Hospital_UpazilaId",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Branch_DistrictId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_DivisionId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_UpazilaId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "UpazilaId",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "UpazilaId",
                table: "Branch");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Hospital",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Upazilla",
                table: "Hospital",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Branch",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Upazila",
                table: "Branch",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }
    }
}
