using Microsoft.EntityFrameworkCore.Migrations;

namespace NRAKOProjektWeb.Data.Migrations
{
    public partial class EditUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37520947-b0b3-410f-b84c-92f27dc9d3fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c89cce9b-ce52-4772-9b96-6d5a0bfaa0b9");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9be7fa1a-7cd1-4ad7-93bb-c9af22e29cea", "c88ef7b2-a7ec-4e2f-962a-9f48bfbacb6d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b35c482-ba9d-437d-8dcd-24b826ae955d", "ef917060-ca4c-4869-bff4-39d1a952fd10", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b35c482-ba9d-437d-8dcd-24b826ae955d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9be7fa1a-7cd1-4ad7-93bb-c9af22e29cea");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37520947-b0b3-410f-b84c-92f27dc9d3fa", "a8b21350-30e8-48b5-a372-37f6b1634fcf", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c89cce9b-ce52-4772-9b96-6d5a0bfaa0b9", "3d23f6fa-a028-44aa-a5a3-8fc186a490ef", "User", "USER" });
        }
    }
}
