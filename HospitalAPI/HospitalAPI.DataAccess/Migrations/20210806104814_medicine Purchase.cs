using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class medicinePurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_MedicinePurchase_MedicinePurchaseId",
                table: "Medicine");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_MedicinePurchaseId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "MedicinePurchaseId",
                table: "Medicine");

            migrationBuilder.CreateTable(
                name: "MedicinePurchasePerUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenericName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MedicinePurchaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePurchasePerUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicinePurchasePerUnit_MedicinePurchase_MedicinePurchaseId",
                        column: x => x.MedicinePurchaseId,
                        principalTable: "MedicinePurchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePurchasePerUnit_MedicinePurchaseId",
                table: "MedicinePurchasePerUnit",
                column: "MedicinePurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicinePurchasePerUnit");

            migrationBuilder.AddColumn<int>(
                name: "MedicinePurchaseId",
                table: "Medicine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_MedicinePurchaseId",
                table: "Medicine",
                column: "MedicinePurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_MedicinePurchase_MedicinePurchaseId",
                table: "Medicine",
                column: "MedicinePurchaseId",
                principalTable: "MedicinePurchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
