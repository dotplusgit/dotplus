using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class VitalReplaceWithPhysicalstat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientVital");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "PhysicalState",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisitEntryId",
                table: "PhysicalState",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "BMI",
                table: "PhysicalState",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightFeet",
                table: "PhysicalState",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightInches",
                table: "PhysicalState",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hip",
                table: "PhysicalState",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLatest",
                table: "PhysicalState",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId1",
                table: "PhysicalState",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PulseRate",
                table: "PhysicalState",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SpO2",
                table: "PhysicalState",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Waist",
                table: "PhysicalState",
                type: "nvarchar(max)",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalState_Patient_PatientId1",
                table: "PhysicalState");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalState_PatientId1",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "BMI",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "HeightFeet",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "HeightInches",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "Hip",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "IsLatest",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "PulseRate",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "SpO2",
                table: "PhysicalState");

            migrationBuilder.DropColumn(
                name: "Waist",
                table: "PhysicalState");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "PhysicalState",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisitEntryId",
                table: "PhysicalState",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PatientVital",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BMI = table.Column<double>(type: "float", nullable: true),
                    HeightFeet = table.Column<int>(type: "int", nullable: true),
                    HeightInches = table.Column<int>(type: "int", nullable: true),
                    Hip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    IsLatest = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PulseRate = table.Column<double>(type: "float", nullable: true),
                    SpO2 = table.Column<double>(type: "float", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Waist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVital", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVital_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientVital_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientVital_HospitalId",
                table: "PatientVital",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVital_PatientId",
                table: "PatientVital",
                column: "PatientId");
        }
    }
}
