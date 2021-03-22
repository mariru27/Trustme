using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class UserKey_KeysCollenction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKey_Key_IdUserKey",
                table: "UserKey");

            migrationBuilder.AlterColumn<int>(
                name: "IdUserKey",
                table: "UserKey",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserKeyIdUserKey",
                table: "Key",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Key_UserKeyIdUserKey",
                table: "Key",
                column: "UserKeyIdUserKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Key_UserKey_UserKeyIdUserKey",
                table: "Key",
                column: "UserKeyIdUserKey",
                principalTable: "UserKey",
                principalColumn: "IdUserKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Key_UserKey_UserKeyIdUserKey",
                table: "Key");

            migrationBuilder.DropIndex(
                name: "IX_Key_UserKeyIdUserKey",
                table: "Key");

            migrationBuilder.DropColumn(
                name: "UserKeyIdUserKey",
                table: "Key");

            migrationBuilder.AlterColumn<int>(
                name: "IdUserKey",
                table: "UserKey",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserKey_Key_IdUserKey",
                table: "UserKey",
                column: "IdUserKey",
                principalTable: "Key",
                principalColumn: "KeyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
