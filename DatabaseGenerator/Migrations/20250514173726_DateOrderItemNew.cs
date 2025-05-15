using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class DateOrderItemNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CuttingCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectricaCabinetCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineAssembled",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineReleased",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineWiredAndTested",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MetalCliningCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaintingCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TechnicalDrawingCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransportOrderedDate",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "TransportOrderedDate",
                table: "OrderItems");
        }
    }
}
