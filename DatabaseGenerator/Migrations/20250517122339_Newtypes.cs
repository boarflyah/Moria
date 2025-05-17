using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class Newtypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ControlCabinetWorkEndDate",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ControlCabinetWorkStartDate",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectricalCabinetId",
                table: "OrderItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectricalDiagramCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectricianId",
                table: "OrderItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ElectricalCabinet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    LockedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricalCabinet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ElectricalCabinetId",
                table: "OrderItems",
                column: "ElectricalCabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ElectricianId",
                table: "OrderItems",
                column: "ElectricianId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ElectricalCabinet_ElectricalCabinetId",
                table: "OrderItems",
                column: "ElectricalCabinetId",
                principalTable: "ElectricalCabinet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Employees_ElectricianId",
                table: "OrderItems",
                column: "ElectricianId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ElectricalCabinet_ElectricalCabinetId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Employees_ElectricianId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "ElectricalCabinet");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ElectricalCabinetId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ElectricianId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ControlCabinetWorkEndDate",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ControlCabinetWorkStartDate",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricalCabinetId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricalDiagramCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricianId",
                table: "OrderItems");
        }
    }
}
