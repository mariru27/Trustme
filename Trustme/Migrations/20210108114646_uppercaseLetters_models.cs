using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class uppercaseLetters_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "User",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "secondName",
                table: "User",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "mail",
                table: "User",
                newName: "Mail");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "User",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "confirmPassword",
                table: "User",
                newName: "ConfirmPassword");

            migrationBuilder.RenameColumn(
                name: "keySize",
                table: "Key",
                newName: "KeySize");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Key",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "certificateName",
                table: "Key",
                newName: "CertificateName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "User",
                newName: "secondName");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "User",
                newName: "mail");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "ConfirmPassword",
                table: "User",
                newName: "confirmPassword");

            migrationBuilder.RenameColumn(
                name: "KeySize",
                table: "Key",
                newName: "keySize");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Key",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CertificateName",
                table: "Key",
                newName: "certificateName");
        }
    }
}
