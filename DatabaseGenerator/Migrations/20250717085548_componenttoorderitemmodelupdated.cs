using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class componenttoorderitemmodelupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriveId",
                table: "ComponentToOrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ComponentToOrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentToOrderItems_DriveId",
                table: "ComponentToOrderItems",
                column: "DriveId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentToOrderItems_Drives_DriveId",
                table: "ComponentToOrderItems",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentToOrderItems_Drives_DriveId",
                table: "ComponentToOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_ComponentToOrderItems_DriveId",
                table: "ComponentToOrderItems");

            migrationBuilder.DropColumn(
                name: "DriveId",
                table: "ComponentToOrderItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ComponentToOrderItems");
        }
    }
}
