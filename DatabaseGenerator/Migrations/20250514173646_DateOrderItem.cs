using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class DateOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuttingCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricaCabinetCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineAssembled",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineReleased",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineWiredAndTested",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MetalCliningCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PaintingCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TechnicalDrawingCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TransportOrdered",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CuttingCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ElectricaCabinetCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MachineAssembled",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MachineReleased",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MachineWiredAndTested",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MetalCliningCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PaintingCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TechnicalDrawingCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TransportOrdered",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
