using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class delete_ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyId",
                table: "UserKey");

            migrationBuilder.DropColumn(
                name: "UserKeyId",
                table: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyId",
                table: "UserKey",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserKeyId",
                table: "Key",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
