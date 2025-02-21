using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class NewStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Components_ComponentId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ElectricalDescription",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "ComponentId",
                table: "OrderItems",
                newName: "DriveId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ComponentId",
                table: "OrderItems",
                newName: "IX_OrderItems_DriveId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "OrderItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ComponentColorId",
                table: "Components",
                type: "integer",
                nullable: true);

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
                name: "ComponentOrderItem",
                columns: table => new
                {
                    ComponentsId = table.Column<int>(type: "integer", nullable: false),
                    OrderItemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOrderItem", x => new { x.ComponentsId, x.OrderItemsId });
                    table.ForeignKey(
                        name: "FK_ComponentOrderItem_Components_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentOrderItem_OrderItems_OrderItemsId",
                        column: x => x.OrderItemsId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentToOrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ColorId = table.Column<int>(type: "integer", nullable: true),
                    ElectricialDescription = table.Column<string>(type: "text", nullable: true),
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    OrderItemId = table.Column<int>(type: "integer", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    LockedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentToOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentToOrderItem_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentToOrderItem_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentToOrderItem_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriveToComponent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DriveId = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    LockedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveToComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriveToComponent_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriveToComponent_Drives_DriveId",
                        column: x => x.DriveId,
                        principalTable: "Drives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentColorId",
                table: "Components",
                column: "ComponentColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDrive_DrivesId",
                table: "ComponentDrive",
                column: "DrivesId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOrderItem_OrderItemsId",
                table: "ComponentOrderItem",
                column: "OrderItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentToOrderItem_ColorId",
                table: "ComponentToOrderItem",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentToOrderItem_ComponentId",
                table: "ComponentToOrderItem",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentToOrderItem_OrderItemId",
                table: "ComponentToOrderItem",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveToComponent_ComponentId",
                table: "DriveToComponent",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveToComponent_DriveId",
                table: "DriveToComponent",
                column: "DriveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Colors_ComponentColorId",
                table: "Components",
                column: "ComponentColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Drives_DriveId",
                table: "OrderItems",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Colors_ComponentColorId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Drives_DriveId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "ComponentDrive");

            migrationBuilder.DropTable(
                name: "ComponentOrderItem");

            migrationBuilder.DropTable(
                name: "ComponentToOrderItem");

            migrationBuilder.DropTable(
                name: "DriveToComponent");

            migrationBuilder.DropIndex(
                name: "IX_Components_ComponentColorId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ComponentColorId",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "DriveId",
                table: "OrderItems",
                newName: "ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_DriveId",
                table: "OrderItems",
                newName: "IX_OrderItems_ComponentId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricalDescription",
                table: "Components",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Components_ComponentId",
                table: "OrderItems",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
