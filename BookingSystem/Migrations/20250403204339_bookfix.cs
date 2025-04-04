using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class bookfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_HotelId",
                table: "Booking",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Hotel_HotelId",
                table: "Booking",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Hotel_HotelId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_HotelId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Booking");
        }
    }
}
