using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class PatientAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    VisitEntryId = table.Column<int>(type: "int", nullable: false),
                    DoctorsObservation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalStateId = table.Column<int>(type: "int", nullable: false),
                    AdviceMedication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdviceTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescription_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescription_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescription_PhysicalState_PhysicalStateId",
                        column: x => x.PhysicalStateId,
                        principalTable: "PhysicalState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescription_VisitEntry_VisitEntryId",
                        column: x => x.VisitEntryId,
                        principalTable: "VisitEntry",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_HospitalId",
                table: "Prescription",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PhysicalStateId",
                table: "Prescription",
                column: "PhysicalStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_VisitEntryId",
                table: "Prescription",
                column: "VisitEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescription");
        }
    }
}
