using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class medicineandmedicinestockaddedtodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Hospital_HospitalId",
                table: "Medicine");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineForPrescription_Medicine_MedicineId",
                table: "MedicineForPrescription");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_HospitalId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Medicine");

            migrationBuilder.CreateTable(
                name: "MedicineStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<double>(type: "float", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineStock_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineStock_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineStock_HospitalId",
                table: "MedicineStock",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineStock_MedicineId",
                table: "MedicineStock",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineForPrescription_MedicineStock_MedicineId",
                table: "MedicineForPrescription",
                column: "MedicineId",
                principalTable: "MedicineStock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineForPrescription_MedicineStock_MedicineId",
                table: "MedicineForPrescription");

            migrationBuilder.DropTable(
                name: "MedicineStock");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Medicine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Medicine",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Unit",
                table: "Medicine",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "Medicine",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_HospitalId",
                table: "Medicine",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Hospital_HospitalId",
                table: "Medicine",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineForPrescription_Medicine_MedicineId",
                table: "MedicineForPrescription",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
