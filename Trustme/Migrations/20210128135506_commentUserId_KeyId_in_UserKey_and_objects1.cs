using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class commentUserId_KeyId_in_UserKey_and_objects1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey");

            migrationBuilder.DropIndex(
                name: "IX_UserKey_UserId",
                table: "UserKey");

            migrationBuilder.AlterColumn<int>(
                name: "UserKeyId",
                table: "UserKey",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserKeyId",
                table: "UserKey",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserKey_UserId",
                table: "UserKey",
                column: "UserKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserKey_User_UserId",
                table: "UserKey",
                column: "UserKeyId",
                principalTable: "User",
                principalColumn: "UserKeyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
