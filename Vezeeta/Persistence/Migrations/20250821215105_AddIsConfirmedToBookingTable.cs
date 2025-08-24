using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsConfirmedToBookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Bookings");
        }
    }
}
