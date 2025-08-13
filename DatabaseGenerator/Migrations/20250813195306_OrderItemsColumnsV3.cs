using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemsColumnsV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedMachineAssembledAll",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedMachineAssembledAll",
                table: "OrderItems");
        }
    }
}
