using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class medicineaddedtomedicineforprescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineForPrescription_MedicineStock_MedicineId",
                table: "MedicineForPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineForPrescription_Medicine_MedicineId",
                table: "MedicineForPrescription",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineForPrescription_Medicine_MedicineId",
                table: "MedicineForPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineForPrescription_MedicineStock_MedicineId",
                table: "MedicineForPrescription",
                column: "MedicineId",
                principalTable: "MedicineStock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
