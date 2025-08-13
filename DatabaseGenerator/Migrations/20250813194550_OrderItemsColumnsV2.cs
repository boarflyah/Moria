using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemsColumnsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MachineAssembledAllCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MachineAssembledAllStarted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachineAssembledAllCompleted",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MachineAssembledAllStarted",
                table: "OrderItems");
        }
    }
}
