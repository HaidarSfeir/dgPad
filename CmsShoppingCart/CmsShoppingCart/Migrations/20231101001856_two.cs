using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsShoppingCart.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "73ce7cd0-6adb-4e81-871a-e74e38b51686");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c0fbca3-cafa-4918-8b54-0583bad434a0", "AQAAAAEAACcQAAAAEO9r3EHD6tzLb4DP3jRjSofubYYs3vz1ZSpd1d0CFFeec8YjvV6adWGmdyEh9fNnEw==", "a9a8b190-4f18-4b52-98da-65bb95eb6b99" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "cdfd0fb8-5126-42aa-8540-f363594fd714");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de2d7b3e-a8b1-4dc7-99b8-8171ddf77cc5", "AQAAAAEAACcQAAAAEGVh10JGs1rS9drvgohGRJ81BG27vbE9YcRtxIv2YOJGaulMDywvAJ6Mf/uB3Asn5g==", "6aed0efd-bff3-481f-b055-2b42a886b669" });
        }
    }
}
