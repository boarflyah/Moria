﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    /// <inheritdoc />
    public partial class WeldingCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "WeldingCompleted",
                table: "OrderItems",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeldingCompleted",
                table: "OrderItems");
        }
    }
}
