using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class addedgeneralexamination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "GeneralExamination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription",
                column: "GeneralExaminationId",
                unique: true,
                filter: "[GeneralExaminationId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "GeneralExamination");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription",
                column: "GeneralExaminationId");
        }
    }
}
