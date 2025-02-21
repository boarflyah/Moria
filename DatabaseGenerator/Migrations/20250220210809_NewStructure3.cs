using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class NewStructure3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_OrderItems_OrderItemId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Components_ProductNameId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Components_OrderItemId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "ProductNameId",
                table: "OrderItems",
                newName: "ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductNameId",
                table: "OrderItems",
                newName: "IX_OrderItems_ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Components_ComponentId",
                table: "OrderItems",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Components_ComponentId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ComponentId",
                table: "OrderItems",
                newName: "ProductNameId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ComponentId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductNameId");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Components",
                type: "integer",
                nullable: true);

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
    }
}
