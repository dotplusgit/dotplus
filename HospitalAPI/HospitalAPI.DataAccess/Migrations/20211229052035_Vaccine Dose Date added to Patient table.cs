using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class VaccineDoseDateaddedtoPatienttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BosterDoseDate",
                table: "Patient",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstDoseDate",
                table: "Patient",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecondDoseDate",
                table: "Patient",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BosterDoseDate",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "FirstDoseDate",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "SecondDoseDate",
                table: "Patient");
        }
    }
}
