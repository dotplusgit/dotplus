using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class hospitalremovefrombranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Hospital_HospitalId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_HospitalId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Branch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Branch",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_HospitalId",
                table: "Branch",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Hospital_HospitalId",
                table: "Branch",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
