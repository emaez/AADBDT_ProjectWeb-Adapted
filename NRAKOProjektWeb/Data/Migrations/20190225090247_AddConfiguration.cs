using Microsoft.EntityFrameworkCore.Migrations;

namespace NRAKOProjektWeb.Data.Migrations
{
    public partial class AddConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c03fcd9-091a-470e-9c68-884e54afd3b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cacf2449-76af-43e3-a8c5-9841a7a710f7");

            migrationBuilder.CreateTable(
                name: "ConfigurationEntities",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationEntities", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1cbe4f52-b6f7-478d-a443-eeff033b922e", "98735956-990b-4249-9e0d-74fcc3793bcf", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71b15592-c00d-4925-ac19-f47dd2ff615d", "2c0e79c2-95e7-4761-ba44-fa8e93791c3c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "ConfigurationEntities",
                columns: new[] { "Name", "Value" },
                values: new object[] { "StorageType", "Local" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationEntities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cbe4f52-b6f7-478d-a443-eeff033b922e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71b15592-c00d-4925-ac19-f47dd2ff615d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cacf2449-76af-43e3-a8c5-9841a7a710f7", "b8479de5-4be1-4e46-8e5d-09f95ac3c7bc", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c03fcd9-091a-470e-9c68-884e54afd3b1", "e2b6612a-e2d5-45dc-b755-f7115e7d2ffb", "User", "USER" });
        }
    }
}
