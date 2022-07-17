using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class add_extra_fieldtoprescriptionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralExamination");

            migrationBuilder.AddColumn<string>(
                name: "DX",
                table: "Prescription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MH",
                table: "Prescription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OH",
                table: "Prescription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DX",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "MH",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "OH",
                table: "Prescription");

            migrationBuilder.CreateTable(
                name: "GeneralExamination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abdomen = table.Column<bool>(type: "bit", nullable: true),
                    Anemia = table.Column<bool>(type: "bit", nullable: true),
                    Appearance = table.Column<bool>(type: "bit", nullable: true),
                    Cyanosis = table.Column<bool>(type: "bit", nullable: true),
                    Dehydration = table.Column<bool>(type: "bit", nullable: true),
                    Edema = table.Column<bool>(type: "bit", nullable: true),
                    Heart = table.Column<bool>(type: "bit", nullable: true),
                    Jundice = table.Column<bool>(type: "bit", nullable: true),
                    KUB = table.Column<bool>(type: "bit", nullable: true),
                    Lung = table.Column<bool>(type: "bit", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralExamination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralExamination_Prescription_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExamination_PrescriptionId",
                table: "GeneralExamination",
                column: "PrescriptionId",
                unique: true,
                filter: "[PrescriptionId] IS NOT NULL");
        }
    }
}
