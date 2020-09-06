using Microsoft.EntityFrameworkCore.Migrations;

namespace NRAKOProjektWeb.Data.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37520947-b0b3-410f-b84c-92f27dc9d3fa", "a8b21350-30e8-48b5-a372-37f6b1634fcf", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c89cce9b-ce52-4772-9b96-6d5a0bfaa0b9", "3d23f6fa-a028-44aa-a5a3-8fc186a490ef", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37520947-b0b3-410f-b84c-92f27dc9d3fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c89cce9b-ce52-4772-9b96-6d5a0bfaa0b9");
        }
    }
}
