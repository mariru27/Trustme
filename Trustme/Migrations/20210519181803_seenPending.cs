using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class seenPending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Pending",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Pending");
        }
    }
}
