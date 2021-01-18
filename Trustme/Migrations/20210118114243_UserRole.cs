using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class UserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleIdRole",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleIdRole",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleIdRole",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleIdRole",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleIdRole",
                table: "User",
                column: "UserRoleIdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_UserRoleIdRole",
                table: "User",
                column: "UserRoleIdRole",
                principalTable: "Role",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_UserRoleIdRole",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserRoleIdRole",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserRoleIdRole",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "RoleIdRole",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleIdRole",
                table: "User",
                column: "RoleIdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleIdRole",
                table: "User",
                column: "RoleIdRole",
                principalTable: "Role",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
