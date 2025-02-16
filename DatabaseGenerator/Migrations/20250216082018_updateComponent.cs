using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class updateComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComponentProductId",
                table: "Components",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentProductId",
                table: "Components",
                column: "ComponentProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Products_ComponentProductId",
                table: "Components",
                column: "ComponentProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Products_ComponentProductId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_ComponentProductId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ComponentProductId",
                table: "Components");
        }
    }
}
