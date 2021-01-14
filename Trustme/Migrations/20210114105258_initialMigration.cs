using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleName",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleName",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleRoleName",
                table: "User",
                column: "UserRoleRoleName");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_UserRoleRoleName",
                table: "User",
                column: "UserRoleRoleName",
                principalTable: "Role",
                principalColumn: "RoleName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_UserRoleRoleName",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserRoleRoleName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleName",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "User",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleName",
                table: "User",
                column: "RoleName");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleName",
                table: "User",
                column: "RoleName",
                principalTable: "Role",
                principalColumn: "RoleName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
