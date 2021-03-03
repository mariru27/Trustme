using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class usernamesent_signed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SentFromUsername",
                table: "UnsignedDocuments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignedByUsername",
                table: "SignedDocuments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentFromUsername",
                table: "UnsignedDocuments");

            migrationBuilder.DropColumn(
                name: "SignedByUsername",
                table: "SignedDocuments");
        }
    }
}
