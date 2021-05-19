using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class blocked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Pending",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Pending");
        }
    }
}
