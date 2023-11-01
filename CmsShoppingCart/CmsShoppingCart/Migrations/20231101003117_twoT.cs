using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsShoppingCart.Migrations
{
    public partial class twoT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "11a37503-43f9-4a96-8aba-51e55a936e14");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e15592e-be7e-41bd-8abc-074ca7dd08d7", "AQAAAAEAACcQAAAAECU9Q7xL1kEQON4TmWkGU87grq1divRMB4dbcbqMfcogB4X3EgZBMTFDjZQ6qdcvAQ==", "a9d55e74-ae8c-40c6-affe-95d0ffd68f74" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
