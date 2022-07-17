using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class Designationmaximum40charecter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_GeneralExamination_GeneralExaminationId",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "GeneralExaminationId",
                table: "Prescription");

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "GeneralExamination",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExamination_PrescriptionId",
                table: "GeneralExamination",
                column: "PrescriptionId",
                unique: true,
                filter: "[PrescriptionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralExamination_Prescription_PrescriptionId",
                table: "GeneralExamination",
                column: "PrescriptionId",
                principalTable: "Prescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralExamination_Prescription_PrescriptionId",
                table: "GeneralExamination");

            migrationBuilder.DropIndex(
                name: "IX_GeneralExamination_PrescriptionId",
                table: "GeneralExamination");

            migrationBuilder.AddColumn<int>(
                name: "GeneralExaminationId",
                table: "Prescription",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "GeneralExamination",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription",
                column: "GeneralExaminationId",
                unique: true,
                filter: "[GeneralExaminationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_GeneralExamination_GeneralExaminationId",
                table: "Prescription",
                column: "GeneralExaminationId",
                principalTable: "GeneralExamination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
