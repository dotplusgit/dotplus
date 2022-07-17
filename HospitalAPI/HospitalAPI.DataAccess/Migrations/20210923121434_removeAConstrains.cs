using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class removeAConstrains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalState_Patient_PatientId1",
                table: "PhysicalState");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalState_PatientId1",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "PhysicalState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId1",
                table: "PhysicalState",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalState_PatientId1",
                table: "PhysicalState",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalState_Patient_PatientId1",
                table: "PhysicalState",
                column: "PatientId1",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
