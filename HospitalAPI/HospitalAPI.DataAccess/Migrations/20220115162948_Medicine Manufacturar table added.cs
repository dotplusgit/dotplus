using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalAPI.DataAccess.Migrations
{
    public partial class MedicineManufacturartableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturar",
                table: "Medicine");

            migrationBuilder.AddColumn<int>(
                name: "MedicineManufacturarId",
                table: "Medicine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicineManufacturar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineManufacturar", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_MedicineManufacturarId",
                table: "Medicine",
                column: "MedicineManufacturarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_MedicineManufacturar_MedicineManufacturarId",
                table: "Medicine",
                column: "MedicineManufacturarId",
                principalTable: "MedicineManufacturar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_MedicineManufacturar_MedicineManufacturarId",
                table: "Medicine");

            migrationBuilder.DropTable(
                name: "MedicineManufacturar");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_MedicineManufacturarId",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "MedicineManufacturarId",
                table: "Medicine");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturar",
                table: "Medicine",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }
    }
}
