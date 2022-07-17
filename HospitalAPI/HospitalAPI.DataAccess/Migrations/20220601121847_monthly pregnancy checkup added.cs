using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class monthlypregnancycheckupadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyCheckupPregnancy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PregnancyId = table.Column<int>(type: "int", nullable: false),
                    IsCheckedUp = table.Column<bool>(type: "bit", nullable: false),
                    CheckupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyCheckupPregnancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyCheckupPregnancy_Pregnancy_PregnancyId",
                        column: x => x.PregnancyId,
                        principalTable: "Pregnancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyCheckupPregnancy_PregnancyId",
                table: "MonthlyCheckupPregnancy",
                column: "PregnancyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyCheckupPregnancy");
        }
    }
}
