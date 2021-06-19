using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Key_UserKey_UserKeyIdUserKey",
                table: "Key");

            migrationBuilder.DropForeignKey(
                name: "FK_Pending_User_UserId",
                table: "Pending");

            migrationBuilder.DropForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSignedDocument_User_UserId",
                table: "UserSignedDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUnsignedDocument_User_UserId",
                table: "UserUnsignedDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_Key_UserKey_UserKeyIdUserKey",
                table: "Key",
                column: "UserKeyIdUserKey",
                principalTable: "UserKey",
                principalColumn: "IdUserKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Pending_User_UserId",
                table: "Pending",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSignedDocument_User_UserId",
                table: "UserSignedDocument",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUnsignedDocument_User_UserId",
                table: "UserUnsignedDocument",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Key_UserKey_UserKeyIdUserKey",
                table: "Key");

            migrationBuilder.DropForeignKey(
                name: "FK_Pending_User_UserId",
                table: "Pending");

            migrationBuilder.DropForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSignedDocument_User_UserId",
                table: "UserSignedDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUnsignedDocument_User_UserId",
                table: "UserUnsignedDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_Key_UserKey_UserKeyIdUserKey",
                table: "Key",
                column: "UserKeyIdUserKey",
                principalTable: "UserKey",
                principalColumn: "IdUserKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pending_User_UserId",
                table: "Pending",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSignedDocument_User_UserId",
                table: "UserSignedDocument",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUnsignedDocument_User_UserId",
                table: "UserUnsignedDocument",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
