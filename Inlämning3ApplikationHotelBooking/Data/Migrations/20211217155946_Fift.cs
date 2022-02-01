using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlämning3ApplikationHotelBooking.Data.Migrations
{
    public partial class Fift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBokked",
                table: "Booking",
                newName: "DateBooked");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DateBokked",
                table: "Booking",
                newName: "IX_Booking_DateBooked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBooked",
                table: "Booking",
                newName: "DateBokked");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DateBooked",
                table: "Booking",
                newName: "IX_Booking_DateBokked");
        }
    }
}
