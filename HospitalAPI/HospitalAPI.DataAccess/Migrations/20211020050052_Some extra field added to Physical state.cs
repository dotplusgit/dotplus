using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class SomeextrafieldaddedtoPhysicalstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Anemia",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BB",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dehydration",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HL",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Jaundice",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PA",
                table: "PhysicalState",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anemia",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "BB",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "Dehydration",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "HL",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "Jaundice",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "PA",
                table: "PhysicalState");
        }
    }
}
