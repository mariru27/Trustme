using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleidRole",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleidRole",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RoleidRole",
                table: "User");

            migrationBuilder.DropColumn(
                name: "idRole",
                table: "User");

            migrationBuilder.DropColumn(
                name: "idRole",
                table: "Role");

            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleName",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleName");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleName",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "RoleidRole",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "idRole",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "idRole",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "idRole");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleidRole",
                table: "User",
                column: "RoleidRole");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleidRole",
                table: "User",
                column: "RoleidRole",
                principalTable: "Role",
                principalColumn: "idRole",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
