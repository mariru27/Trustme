using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class UserId_KeyId_in_UserKey_and_objects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserKey_KeyId",
                table: "UserKey",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKey_UserId",
                table: "UserKey",
                column: "UserKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserKey_Key_KeyId",
                table: "UserKey",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "KeyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey",
                column: "UserKeyId",
                principalTable: "User",
                principalColumn: "UserKeyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKey_Key_KeyId",
                table: "UserKey");

            migrationBuilder.DropForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey");

            migrationBuilder.DropIndex(
                name: "IX_UserKey_KeyId",
                table: "UserKey");

            migrationBuilder.DropIndex(
                name: "IX_UserKey_UserId",
                table: "UserKey");
        }
    }
}
