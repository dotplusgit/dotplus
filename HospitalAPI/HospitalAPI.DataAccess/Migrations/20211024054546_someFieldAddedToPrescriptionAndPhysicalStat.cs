using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class someFieldAddedToPrescriptionAndPhysicalStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PA",
                table: "PhysicalState",
                newName: "Lung");

            migrationBuilder.RenameColumn(
                name: "HL",
                table: "PhysicalState",
                newName: "KUB");

            migrationBuilder.RenameColumn(
                name: "BB",
                table: "PhysicalState",
                newName: "Heart");

            migrationBuilder.AddColumn<string>(
                name: "AllergicHistory",
                table: "Prescription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyHistory",
                table: "Prescription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryOfPastIllness",
                table: "Prescription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemicExamination",
                table: "Prescription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abdomen",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Appearance",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cyanosis",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Edema",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllergicHistory",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "FamilyHistory",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "HistoryOfPastIllness",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "SystemicExamination",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "Abdomen",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "Appearance",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "Cyanosis",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "Edema",
                table: "PhysicalState");

            migrationBuilder.RenameColumn(
                name: "Lung",
                table: "PhysicalState",
                newName: "PA");

            migrationBuilder.RenameColumn(
                name: "KUB",
                table: "PhysicalState",
                newName: "HL");

            migrationBuilder.RenameColumn(
                name: "Heart",
                table: "PhysicalState",
                newName: "BB");
        }
    }
}
