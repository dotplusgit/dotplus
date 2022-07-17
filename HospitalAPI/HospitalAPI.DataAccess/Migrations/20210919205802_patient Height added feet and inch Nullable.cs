using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class patientHeightaddedfeetandinchNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "PatientVital");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "PatientVital",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightFeet",
                table: "PatientVital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightInches",
                table: "PatientVital",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightFeet",
                table: "PatientVital");

            migrationBuilder.DropColumn(
                name: "HeightInches",
                table: "PatientVital");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "PatientVital",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height",
                table: "PatientVital",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
