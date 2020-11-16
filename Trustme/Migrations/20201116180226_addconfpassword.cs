using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class addconfpassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "confirmPassword",
                table: "User",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confirmPassword",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "age",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
