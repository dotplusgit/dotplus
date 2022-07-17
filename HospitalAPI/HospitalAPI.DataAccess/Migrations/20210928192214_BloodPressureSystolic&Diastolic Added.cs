using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class BloodPressureSystolicDiastolicAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BloodPressure",
                table: "PhysicalState",
                newName: "BloodPressureSystolic");

            migrationBuilder.AddColumn<string>(
                name: "BloodPressureDiastolic",
                table: "PhysicalState",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodPressureDiastolic",
                table: "PhysicalState");

            migrationBuilder.RenameColumn(
                name: "BloodPressureSystolic",
                table: "PhysicalState",
                newName: "BloodPressure");
        }
    }
}
