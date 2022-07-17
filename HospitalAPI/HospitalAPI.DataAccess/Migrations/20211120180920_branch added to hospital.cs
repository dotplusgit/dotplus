using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class branchaddedtohospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_BranchId",
                table: "Hospital",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_Branch_BranchId",
                table: "Hospital",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_Branch_BranchId",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Hospital_BranchId",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Hospital");
        }
    }
}
