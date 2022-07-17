using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class generalExaminationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralExaminationId",
                table: "Prescription",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GeneralExamination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionId = table.Column<int>(type: "int", nullable: true),
                    Appearance = table.Column<bool>(type: "bit", nullable: false),
                    Anemia = table.Column<bool>(type: "bit", nullable: false),
                    Jundice = table.Column<bool>(type: "bit", nullable: false),
                    Dehydration = table.Column<bool>(type: "bit", nullable: false),
                    Edema = table.Column<bool>(type: "bit", nullable: false),
                    Cyanosis = table.Column<bool>(type: "bit", nullable: false),
                    Heart = table.Column<bool>(type: "bit", nullable: false),
                    Lung = table.Column<bool>(type: "bit", nullable: false),
                    Abdomen = table.Column<bool>(type: "bit", nullable: false),
                    KUB = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription",
                column: "GeneralExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExamination_PrescriptionId",
                table: "GeneralExamination",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_GeneralExamination_GeneralExaminationId",
                table: "Prescription",
                column: "GeneralExaminationId",
                principalTable: "GeneralExamination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_GeneralExamination_GeneralExaminationId",
                table: "Prescription");

            migrationBuilder.DropTable(
                name: "GeneralExamination");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_GeneralExaminationId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "GeneralExaminationId",
                table: "Prescription");
        }
    }
}
