using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class newUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentToOrderItem_Colors_ColorId",
                table: "ComponentToOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentToOrderItem_Components_ComponentId",
                table: "ComponentToOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentToOrderItem_OrderItems_OrderItemId",
                table: "ComponentToOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveToComponent_Components_ComponentId",
                table: "DriveToComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveToComponent_Drives_DriveId",
                table: "DriveToComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorGearToDrive_Drives_DriveId",
                table: "MotorGearToDrive");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorGearToDrive_MotorGears_MotorGearId",
                table: "MotorGearToDrive");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Employees_DesignerId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Warehouses_WarehouseId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "ComponentDrive");

            migrationBuilder.DropTable(
                name: "DriveMotorGear");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotorGearToDrive",
                table: "MotorGearToDrive");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriveToComponent",
                table: "DriveToComponent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentToOrderItem",
                table: "ComponentToOrderItem");

            migrationBuilder.RenameTable(
                name: "MotorGearToDrive",
                newName: "MotorGearToDrives");

            migrationBuilder.RenameTable(
                name: "DriveToComponent",
                newName: "DriveToComponents");

            migrationBuilder.RenameTable(
                name: "ComponentToOrderItem",
                newName: "ComponentToOrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_MotorGearToDrive_MotorGearId",
                table: "MotorGearToDrives",
                newName: "IX_MotorGearToDrives_MotorGearId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorGearToDrive_DriveId",
                table: "MotorGearToDrives",
                newName: "IX_MotorGearToDrives_DriveId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveToComponent_DriveId",
                table: "DriveToComponents",
                newName: "IX_DriveToComponents_DriveId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveToComponent_ComponentId",
                table: "DriveToComponents",
                newName: "IX_DriveToComponents_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentToOrderItem_OrderItemId",
                table: "ComponentToOrderItems",
                newName: "IX_ComponentToOrderItems_OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentToOrderItem_ComponentId",
                table: "ComponentToOrderItems",
                newName: "IX_ComponentToOrderItems_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentToOrderItem_ColorId",
                table: "ComponentToOrderItems",
                newName: "IX_ComponentToOrderItems_ColorId");

            migrationBuilder.AddColumn<int>(
                name: "SubiektId",
                table: "Warehouses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubiektId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SubiektId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "OrderItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DesignerId",
                table: "OrderItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SubiektId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubiektId",
                table: "Contacts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotorGearToDrives",
                table: "MotorGearToDrives",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriveToComponents",
                table: "DriveToComponents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentToOrderItems",
                table: "ComponentToOrderItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LastSubiektImport = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    LockedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentToOrderItems_Colors_ColorId",
                table: "ComponentToOrderItems",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentToOrderItems_Components_ComponentId",
                table: "ComponentToOrderItems",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentToOrderItems_OrderItems_OrderItemId",
                table: "ComponentToOrderItems",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveToComponents_Components_ComponentId",
                table: "DriveToComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveToComponents_Drives_DriveId",
                table: "DriveToComponents",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorGearToDrives_Drives_DriveId",
                table: "MotorGearToDrives",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorGearToDrives_MotorGears_MotorGearId",
                table: "MotorGearToDrives",
                column: "MotorGearId",
                principalTable: "MotorGears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Employees_DesignerId",
                table: "OrderItems",
                column: "DesignerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Warehouses_WarehouseId",
                table: "OrderItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentToOrderItems_Colors_ColorId",
                table: "ComponentToOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentToOrderItems_Components_ComponentId",
                table: "ComponentToOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentToOrderItems_OrderItems_OrderItemId",
                table: "ComponentToOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveToComponents_Components_ComponentId",
                table: "DriveToComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveToComponents_Drives_DriveId",
                table: "DriveToComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorGearToDrives_Drives_DriveId",
                table: "MotorGearToDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorGearToDrives_MotorGears_MotorGearId",
                table: "MotorGearToDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Employees_DesignerId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Warehouses_WarehouseId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotorGearToDrives",
                table: "MotorGearToDrives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriveToComponents",
                table: "DriveToComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentToOrderItems",
                table: "ComponentToOrderItems");

            migrationBuilder.DropColumn(
                name: "SubiektId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "SubiektId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubiektId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubiektId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SubiektId",
                table: "Contacts");

            migrationBuilder.RenameTable(
                name: "MotorGearToDrives",
                newName: "MotorGearToDrive");

            migrationBuilder.RenameTable(
                name: "DriveToComponents",
                newName: "DriveToComponent");

            migrationBuilder.RenameTable(
                name: "ComponentToOrderItems",
                newName: "ComponentToOrderItem");

            migrationBuilder.RenameIndex(
                name: "IX_MotorGearToDrives_MotorGearId",
                table: "MotorGearToDrive",
                newName: "IX_MotorGearToDrive_MotorGearId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorGearToDrives_DriveId",
                table: "MotorGearToDrive",
                newName: "IX_MotorGearToDrive_DriveId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveToComponents_DriveId",
                table: "DriveToComponent",
                newName: "IX_DriveToComponent_DriveId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveToComponents_ComponentId",
                table: "DriveToComponent",
                newName: "IX_DriveToComponent_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentToOrderItems_OrderItemId",
                table: "ComponentToOrderItem",
                newName: "IX_ComponentToOrderItem_OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentToOrderItems_ComponentId",
                table: "ComponentToOrderItem",
                newName: "IX_ComponentToOrderItem_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentToOrderItems_ColorId",
                table: "ComponentToOrderItem",
                newName: "IX_ComponentToOrderItem_ColorId");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignerId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotorGearToDrive",
                table: "MotorGearToDrive",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriveToComponent",
                table: "DriveToComponent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentToOrderItem",
                table: "ComponentToOrderItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ComponentDrive",
                columns: table => new
                {
                    ComponentsId = table.Column<int>(type: "integer", nullable: false),
                    DrivesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentDrive", x => new { x.ComponentsId, x.DrivesId });
                    table.ForeignKey(
                        name: "FK_ComponentDrive_Components_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentDrive_Drives_DrivesId",
                        column: x => x.DrivesId,
                        principalTable: "Drives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriveMotorGear",
                columns: table => new
                {
                    DrivesId = table.Column<int>(type: "integer", nullable: false),
                    MotorGearsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveMotorGear", x => new { x.DrivesId, x.MotorGearsId });
                    table.ForeignKey(
                        name: "FK_DriveMotorGear_Drives_DrivesId",
                        column: x => x.DrivesId,
                        principalTable: "Drives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriveMotorGear_MotorGears_MotorGearsId",
                        column: x => x.MotorGearsId,
                        principalTable: "MotorGears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDrive_DrivesId",
                table: "ComponentDrive",
                column: "DrivesId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveMotorGear_MotorGearsId",
                table: "DriveMotorGear",
                column: "MotorGearsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentToOrderItem_Colors_ColorId",
                table: "ComponentToOrderItem",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentToOrderItem_Components_ComponentId",
                table: "ComponentToOrderItem",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentToOrderItem_OrderItems_OrderItemId",
                table: "ComponentToOrderItem",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveToComponent_Components_ComponentId",
                table: "DriveToComponent",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveToComponent_Drives_DriveId",
                table: "DriveToComponent",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorGearToDrive_Drives_DriveId",
                table: "MotorGearToDrive",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorGearToDrive_MotorGears_MotorGearId",
                table: "MotorGearToDrive",
                column: "MotorGearId",
                principalTable: "MotorGears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Employees_DesignerId",
                table: "OrderItems",
                column: "DesignerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Warehouses_WarehouseId",
                table: "OrderItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
