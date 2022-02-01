using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlämning3ApplikationHotelBooking.Data.Migrations
{
    public partial class eight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Booking_DateBooked",
                table: "Booking");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "2735bff1-6a96-419c-9326-c65078c55432");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "166f789d-57bb-40c9-b32a-1b920e94c9e6", "AQAAAAEAACcQAAAAEHSTej33DkiepgSld2XgvsErUS+CA0/PIULr0s2ISUFFZYOJWGIlYd6fuC9V9gvUpQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "7bde63bc-64f0-4593-a1d1-dff2d1c1aa33");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bd5c1e52-7610-415f-b799-d70c657c55de", "AQAAAAEAACcQAAAAELTqY4sz7rrm1z4avNPhnCD85603fzkHWTZqnpF3r7Th5VMz/gFQ0LyPckI9uVms3w==" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_DateBooked",
                table: "Booking",
                column: "DateBooked",
                unique: true);
        }
    }
}
