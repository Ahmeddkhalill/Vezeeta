﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToDoctorTimeSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "DoctorTimeSlots",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "DoctorTimeSlots");
        }
    }
}
