using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class NewProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Contacts_OrderingContactId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Contacts_ReceivingContactId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ReceivingContactId",
                table: "Orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "OrderingContactId",
                table: "Orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Contacts_OrderingContactId",
                table: "Orders",
                column: "OrderingContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Contacts_ReceivingContactId",
                table: "Orders",
                column: "ReceivingContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Contacts_OrderingContactId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Contacts_ReceivingContactId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ReceivingContactId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderingContactId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Contacts_OrderingContactId",
                table: "Orders",
                column: "OrderingContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Contacts_ReceivingContactId",
                table: "Orders",
                column: "ReceivingContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
