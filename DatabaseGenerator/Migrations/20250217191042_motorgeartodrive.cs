using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class motorgeartodrive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drives_Motors_MotorId",
                table: "Drives");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorGears_Drives_DriveId",
                table: "MotorGears");

            migrationBuilder.DropIndex(
                name: "IX_MotorGears_DriveId",
                table: "MotorGears");

            migrationBuilder.DropColumn(
                name: "DriveId",
                table: "MotorGears");

            migrationBuilder.AlterColumn<int>(
                name: "MotorId",
                table: "Drives",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.CreateTable(
                name: "MotorGearToDrive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DriveId = table.Column<int>(type: "integer", nullable: false),
                    MotorGearId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    LockedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorGearToDrive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorGearToDrive_Drives_DriveId",
                        column: x => x.DriveId,
                        principalTable: "Drives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorGearToDrive_MotorGears_MotorGearId",
                        column: x => x.MotorGearId,
                        principalTable: "MotorGears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriveMotorGear_MotorGearsId",
                table: "DriveMotorGear",
                column: "MotorGearsId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorGearToDrive_DriveId",
                table: "MotorGearToDrive",
                column: "DriveId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorGearToDrive_MotorGearId",
                table: "MotorGearToDrive",
                column: "MotorGearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drives_Motors_MotorId",
                table: "Drives",
                column: "MotorId",
                principalTable: "Motors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drives_Motors_MotorId",
                table: "Drives");

            migrationBuilder.DropTable(
                name: "DriveMotorGear");

            migrationBuilder.DropTable(
                name: "MotorGearToDrive");

            migrationBuilder.AddColumn<int>(
                name: "DriveId",
                table: "MotorGears",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MotorId",
                table: "Drives",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotorGears_DriveId",
                table: "MotorGears",
                column: "DriveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drives_Motors_MotorId",
                table: "Drives",
                column: "MotorId",
                principalTable: "Motors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorGears_Drives_DriveId",
                table: "MotorGears",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id");
        }
    }
}
