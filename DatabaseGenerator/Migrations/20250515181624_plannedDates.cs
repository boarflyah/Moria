using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class plannedDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedMachineAssembled",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedMachineWiredAndTested",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedTransport",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedMachineAssembled",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PlannedMachineWiredAndTested",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PlannedTransport",
                table: "OrderItems");
        }
    }
}
