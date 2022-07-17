using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class Medicine_Purchase_Nullable_Entity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchase_Hospital_HospitalId",
                table: "MedicinePurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchase_Patient_PatientId",
                table: "MedicinePurchase");

            migrationBuilder.DropIndex(
                name: "IX_MedicinePurchase_PatientId",
                table: "MedicinePurchase");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "MedicinePurchase");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "MedicinePurchase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "MedicinePurchase",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePurchase_PrescriptionId",
                table: "MedicinePurchase",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchase_Hospital_HospitalId",
                table: "MedicinePurchase",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchase_Prescription_PrescriptionId",
                table: "MedicinePurchase",
                column: "PrescriptionId",
                principalTable: "Prescription",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchase_Hospital_HospitalId",
                table: "MedicinePurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchase_Prescription_PrescriptionId",
                table: "MedicinePurchase");

            migrationBuilder.DropIndex(
                name: "IX_MedicinePurchase_PrescriptionId",
                table: "MedicinePurchase");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "MedicinePurchase");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "MedicinePurchase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "MedicinePurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePurchase_PatientId",
                table: "MedicinePurchase",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchase_Hospital_HospitalId",
                table: "MedicinePurchase",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchase_Patient_PatientId",
                table: "MedicinePurchase",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id");
        }
    }
}
