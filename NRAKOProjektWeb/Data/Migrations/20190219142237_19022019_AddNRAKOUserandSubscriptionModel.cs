using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NRAKOProjektWeb.Data.Migrations
{
    public partial class _19022019_AddNRAKOUserandSubscriptionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionModelId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubscriptionModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MaxUploadSize = table.Column<int>(nullable: false),
                    DailyUploadLimit = table.Column<int>(nullable: false),
                    MaxNumberOfPhotos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionModels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubscriptionModelId",
                table: "AspNetUsers",
                column: "SubscriptionModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SubscriptionModels_SubscriptionModelId",
                table: "AspNetUsers",
                column: "SubscriptionModelId",
                principalTable: "SubscriptionModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SubscriptionModels_SubscriptionModelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SubscriptionModels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SubscriptionModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscriptionModelId",
                table: "AspNetUsers");
        }
    }
}
