using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class BaseModelModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Warehouses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "SteelKinds",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Positions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "OrderItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Motors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "MotorGears",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Drives",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Contacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Components",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Colors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Categories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "SteelKinds");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Motors");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "MotorGears");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Drives");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Categories");
        }
    }
}
