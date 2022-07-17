using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class MedicinePurchaseadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicinePurchaseId",
                table: "Medicine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicinePurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicinePurchase_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinePurchase_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_MedicinePurchaseId",
                table: "Medicine",
                column: "MedicinePurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePurchase_HospitalId",
                table: "MedicinePurchase",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePurchase_PatientId",
                table: "MedicinePurchase",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_MedicinePurchase_MedicinePurchaseId",
                table: "Medicine",
                column: "MedicinePurchaseId",
                principalTable: "MedicinePurchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_MedicinePurchase_MedicinePurchaseId",
                table: "Medicine");

            migrationBuilder.DropTable(
                name: "MedicinePurchase");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_MedicinePurchaseId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "MedicinePurchaseId",
                table: "Medicine");
        }
    }
}
