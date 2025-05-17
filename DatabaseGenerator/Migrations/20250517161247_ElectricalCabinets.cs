using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class ElectricalCabinets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ElectricalCabinet_ElectricalCabinetId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectricalCabinet",
                table: "ElectricalCabinet");

            migrationBuilder.RenameTable(
                name: "ElectricalCabinet",
                newName: "ElectricalCabinets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectricalCabinets",
                table: "ElectricalCabinets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ElectricalCabinets_ElectricalCabinetId",
                table: "OrderItems",
                column: "ElectricalCabinetId",
                principalTable: "ElectricalCabinets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ElectricalCabinets_ElectricalCabinetId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectricalCabinets",
                table: "ElectricalCabinets");

            migrationBuilder.RenameTable(
                name: "ElectricalCabinets",
                newName: "ElectricalCabinet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectricalCabinet",
                table: "ElectricalCabinet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ElectricalCabinet_ElectricalCabinetId",
                table: "OrderItems",
                column: "ElectricalCabinetId",
                principalTable: "ElectricalCabinet",
                principalColumn: "Id");
        }
    }
}
