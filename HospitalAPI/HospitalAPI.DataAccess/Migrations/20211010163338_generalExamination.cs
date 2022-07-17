using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class generalExamination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralExamination_Prescription_PrescriptionId",
                table: "GeneralExamination");

            migrationBuilder.DropIndex(
                name: "IX_GeneralExamination_PrescriptionId",
                table: "GeneralExamination");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "GeneralExamination");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "GeneralExamination",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExamination_PrescriptionId",
                table: "GeneralExamination",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralExamination_Prescription_PrescriptionId",
                table: "GeneralExamination",
                column: "PrescriptionId",
                principalTable: "Prescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
