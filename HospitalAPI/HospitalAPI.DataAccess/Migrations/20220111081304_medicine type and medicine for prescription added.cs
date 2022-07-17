using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class medicinetypeandmedicineforprescriptionadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicineType",
                table: "Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicineForPrescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: true),
                    Dose = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Time = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineForPrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineForPrescription_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicineForPrescription_Prescription_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineForPrescription_MedicineId",
                table: "MedicineForPrescription",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineForPrescription_PrescriptionId",
                table: "MedicineForPrescription",
                column: "PrescriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineForPrescription");

            migrationBuilder.DropColumn(
                name: "MedicineType",
                table: "Medicine");
        }
    }
}
