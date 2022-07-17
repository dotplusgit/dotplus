using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class HospitaladdedtoPregnancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Pregnancy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pregnancy_HospitalId",
                table: "Pregnancy",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregnancy_Hospital_HospitalId",
                table: "Pregnancy",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregnancy_Hospital_HospitalId",
                table: "Pregnancy");

            migrationBuilder.DropIndex(
                name: "IX_Pregnancy_HospitalId",
                table: "Pregnancy");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Pregnancy");
        }
    }
}
