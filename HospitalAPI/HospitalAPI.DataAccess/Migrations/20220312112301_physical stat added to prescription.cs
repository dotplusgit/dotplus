using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class physicalstataddedtoprescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhysicalStateId",
                table: "Prescription",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PhysicalStateId",
                table: "Prescription",
                column: "PhysicalStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_PhysicalState_PhysicalStateId",
                table: "Prescription",
                column: "PhysicalStateId",
                principalTable: "PhysicalState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_PhysicalState_PhysicalStateId",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PhysicalStateId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "PhysicalStateId",
                table: "Prescription");
        }
    }
}
