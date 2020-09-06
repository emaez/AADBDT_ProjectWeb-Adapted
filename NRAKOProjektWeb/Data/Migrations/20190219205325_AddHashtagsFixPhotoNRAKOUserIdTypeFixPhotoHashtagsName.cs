using Microsoft.EntityFrameworkCore.Migrations;

namespace NRAKOProjektWeb.Data.Migrations
{
    public partial class AddHashtagsFixPhotoNRAKOUserIdTypeFixPhotoHashtagsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoHashTags_Hashtag_HashtagId",
                table: "PhotoHashTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoHashTags_Photos_PhotoId",
                table: "PhotoHashTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_NRAKOUserId1",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_NRAKOUserId1",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoHashTags",
                table: "PhotoHashTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hashtag",
                table: "Hashtag");

            migrationBuilder.DropColumn(
                name: "NRAKOUserId1",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "PhotoHashTags",
                newName: "PhotoHashtags");

            migrationBuilder.RenameTable(
                name: "Hashtag",
                newName: "Hashtags");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoHashTags_HashtagId",
                table: "PhotoHashtags",
                newName: "IX_PhotoHashtags_HashtagId");

            migrationBuilder.AlterColumn<string>(
                name: "NRAKOUserId",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoHashtags",
                table: "PhotoHashtags",
                columns: new[] { "PhotoId", "HashtagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hashtags",
                table: "Hashtags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_NRAKOUserId",
                table: "Photos",
                column: "NRAKOUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoHashtags_Hashtags_HashtagId",
                table: "PhotoHashtags",
                column: "HashtagId",
                principalTable: "Hashtags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoHashtags_Photos_PhotoId",
                table: "PhotoHashtags",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_NRAKOUserId",
                table: "Photos",
                column: "NRAKOUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoHashtags_Hashtags_HashtagId",
                table: "PhotoHashtags");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoHashtags_Photos_PhotoId",
                table: "PhotoHashtags");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_NRAKOUserId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_NRAKOUserId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoHashtags",
                table: "PhotoHashtags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hashtags",
                table: "Hashtags");

            migrationBuilder.RenameTable(
                name: "PhotoHashtags",
                newName: "PhotoHashTags");

            migrationBuilder.RenameTable(
                name: "Hashtags",
                newName: "Hashtag");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoHashtags_HashtagId",
                table: "PhotoHashTags",
                newName: "IX_PhotoHashTags_HashtagId");

            migrationBuilder.AlterColumn<int>(
                name: "NRAKOUserId",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NRAKOUserId1",
                table: "Photos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoHashTags",
                table: "PhotoHashTags",
                columns: new[] { "PhotoId", "HashtagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hashtag",
                table: "Hashtag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_NRAKOUserId1",
                table: "Photos",
                column: "NRAKOUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoHashTags_Hashtag_HashtagId",
                table: "PhotoHashTags",
                column: "HashtagId",
                principalTable: "Hashtag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoHashTags_Photos_PhotoId",
                table: "PhotoHashTags",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_NRAKOUserId1",
                table: "Photos",
                column: "NRAKOUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
