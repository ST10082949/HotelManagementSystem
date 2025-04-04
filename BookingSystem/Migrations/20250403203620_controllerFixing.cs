using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class controllerFixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_HotelId",
                table: "Room",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_GuestId",
                table: "Booking",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Guest_GuestId",
                table: "Booking",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel_HotelId",
                table: "Room",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Guest_GuestId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel_HotelId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_HotelId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Booking_GuestId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Booking");

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
