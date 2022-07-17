using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class regaddedtoprescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BMDCRegNo",
                table: "Prescription",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionalEmail",
                table: "Prescription",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BMDCRegNo",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "OptionalEmail",
                table: "Prescription");
        }
    }
}
