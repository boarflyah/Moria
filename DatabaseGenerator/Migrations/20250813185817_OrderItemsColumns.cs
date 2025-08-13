using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CuttingPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CuttingStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectricaCabinetPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectricaCabinetStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectricalDiagramPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectricalDiagramStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineAssembledStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineReleasedPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineReleasedStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineWiredAndTestedStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MetalCliningPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MetalCliningStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaintingPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaintingStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TechnicalDrawingPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TechnicalDrawingStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WeldingPlanned",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WeldingStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuttingPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CuttingStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricaCabinetPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricaCabinetStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricalDiagramPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricalDiagramStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineAssembledStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineReleasedPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineReleasedStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineWiredAndTestedStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MetalCliningPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MetalCliningStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PaintingPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PaintingStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TechnicalDrawingPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TechnicalDrawingStarted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "WeldingPlanned",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "WeldingStarted",
                table: "OrderItems");
        }
    }
}
