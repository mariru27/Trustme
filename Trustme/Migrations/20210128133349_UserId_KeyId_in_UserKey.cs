using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class UserId_KeyId_in_UserKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdKey",
                table: "UserKey");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "UserKey");

            migrationBuilder.AddColumn<int>(
                name: "KeyId",
                table: "UserKey",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserKeyId",
                table: "UserKey",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyId",
                table: "UserKey");

            migrationBuilder.DropColumn(
                name: "UserKeyId",
                table: "UserKey");

            migrationBuilder.AddColumn<int>(
                name: "IdKey",
                table: "UserKey",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "UserKey",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
