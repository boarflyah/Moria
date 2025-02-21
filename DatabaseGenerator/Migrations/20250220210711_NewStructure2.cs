using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class NewStructure2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentOrderItem");

            migrationBuilder.AddColumn<int>(
                name: "ProductNameId",
                table: "OrderItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Components",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductNameId",
                table: "OrderItems",
                column: "ProductNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_OrderItemId",
                table: "Components",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_OrderItems_OrderItemId",
                table: "Components",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Components_ProductNameId",
                table: "OrderItems",
                column: "ProductNameId",
                principalTable: "Components",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_OrderItems_OrderItemId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Components_ProductNameId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductNameId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Components_OrderItemId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ProductNameId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Components");

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

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOrderItem_OrderItemsId",
                table: "ComponentOrderItem",
                column: "OrderItemsId");
        }
    }
}
