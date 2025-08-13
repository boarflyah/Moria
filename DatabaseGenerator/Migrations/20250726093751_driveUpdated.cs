using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class driveUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrakeId",
                table: "Drives",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExternalCoolingId",
                table: "Drives",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PumpId",
                table: "Drives",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplementId",
                table: "Drives",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drives_BrakeId",
                table: "Drives",
                column: "BrakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Drives_ExternalCoolingId",
                table: "Drives",
                column: "ExternalCoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_Drives_PumpId",
                table: "Drives",
                column: "PumpId");

            migrationBuilder.CreateIndex(
                name: "IX_Drives_SupplementId",
                table: "Drives",
                column: "SupplementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drives_Brakes_BrakeId",
                table: "Drives",
                column: "BrakeId",
                principalTable: "Brakes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drives_ExternalCoolings_ExternalCoolingId",
                table: "Drives",
                column: "ExternalCoolingId",
                principalTable: "ExternalCoolings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drives_Pumps_PumpId",
                table: "Drives",
                column: "PumpId",
                principalTable: "Pumps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drives_Supplements_SupplementId",
                table: "Drives",
                column: "SupplementId",
                principalTable: "Supplements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drives_Brakes_BrakeId",
                table: "Drives");

            migrationBuilder.DropForeignKey(
                name: "FK_Drives_ExternalCoolings_ExternalCoolingId",
                table: "Drives");

            migrationBuilder.DropForeignKey(
                name: "FK_Drives_Pumps_PumpId",
                table: "Drives");

            migrationBuilder.DropForeignKey(
                name: "FK_Drives_Supplements_SupplementId",
                table: "Drives");

            migrationBuilder.DropIndex(
                name: "IX_Drives_BrakeId",
                table: "Drives");

            migrationBuilder.DropIndex(
                name: "IX_Drives_ExternalCoolingId",
                table: "Drives");

            migrationBuilder.DropIndex(
                name: "IX_Drives_PumpId",
                table: "Drives");

            migrationBuilder.DropIndex(
                name: "IX_Drives_SupplementId",
                table: "Drives");

            migrationBuilder.DropColumn(
                name: "BrakeId",
                table: "Drives");

            migrationBuilder.DropColumn(
                name: "ExternalCoolingId",
                table: "Drives");

            migrationBuilder.DropColumn(
                name: "PumpId",
                table: "Drives");

            migrationBuilder.DropColumn(
                name: "SupplementId",
                table: "Drives");
        }
    }
}
