using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class CertificateNameAndDescription_KeyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "certificateName",
                table: "Key",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Key",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "certificateName",
                table: "Key");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Key");
        }
    }
}
