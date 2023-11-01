using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsShoppingCart.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "07c7bbee-1471-40ac-b603-f8784e384c0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea72fe0f-40e5-421d-8673-d639d4911c28", "admin", "c75bb825-3f6a-4570-8b24-20fd5ff968ac" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
