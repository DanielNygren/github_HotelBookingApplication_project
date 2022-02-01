using Microsoft.EntityFrameworkCore.Migrations;

namespace Inlämning3ApplikationHotelBooking.Data.Migrations
{
    public partial class Seven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "bd5c1e52-7610-415f-b799-d70c657c55de", true, "admin@gmail.com", "AQAAAAEAACcQAAAAELTqY4sz7rrm1z4avNPhnCD85603fzkHWTZqnpF3r7Th5VMz/gFQ0LyPckI9uVms3w==", "admin@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "045bfc9f-c247-4027-99b6-cff4a7ffaa27");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "96e158d0-c391-45bb-a3d0-5980efb6ff36", false, "admin", "AQAAAAEAACcQAAAAELpSN7kkbTAqzTPyBfaAgVsCF6QSwdN5s8AFyzZFDtx54laUPHA58PNht6ONazimVA==", "admin" });
        }
    }
}
