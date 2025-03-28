using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class NewProp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ElectricialDescription",
                table: "ComponentToOrderItems",
                newName: "ElectricalDescription");

            migrationBuilder.AddColumn<string>(
                name: "SalesOfferLink",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CuttingCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DetailsColorId",
                table: "OrderItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ElectricaCabinetCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ElectricalDescription",
                table: "OrderItems",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "MainColorId",
                table: "OrderItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaintingCompleted",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Power",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "OrderItems",
                type: "text",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DetailsColorId",
                table: "OrderItems",
                column: "DetailsColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MainColorId",
                table: "OrderItems",
                column: "MainColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Colors_DetailsColorId",
                table: "OrderItems",
                column: "DetailsColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Colors_MainColorId",
                table: "OrderItems",
                column: "MainColorId",
                principalTable: "Colors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Colors_DetailsColorId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Colors_MainColorId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_DetailsColorId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_MainColorId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SalesOfferLink",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CuttingCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DetailsColorId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricaCabinetCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricalDescription",
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
                name: "MainColorId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PaintingCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TechnicalDrawingCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TransportOrdered",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ElectricalDescription",
                table: "ComponentToOrderItems",
                newName: "ElectricialDescription");
        }
    }
}
