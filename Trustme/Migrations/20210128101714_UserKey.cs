using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class UserKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserKey",
                columns: table => new
                {
                    IdUserKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(nullable: false),
                    IdKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKey", x => x.IdUserKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserKey");
        }
    }
}
