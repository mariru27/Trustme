using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class idUserKey_delete_from_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUserKey",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUserKey",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
