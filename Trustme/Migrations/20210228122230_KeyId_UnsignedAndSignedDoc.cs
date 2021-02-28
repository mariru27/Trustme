using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class KeyId_UnsignedAndSignedDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyId",
                table: "UnsignedDocuments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KeyId",
                table: "SignedDocuments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnsignedDocuments_KeyId",
                table: "UnsignedDocuments",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_SignedDocuments_KeyId",
                table: "SignedDocuments",
                column: "KeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SignedDocuments_Key_KeyId",
                table: "SignedDocuments",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "KeyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnsignedDocuments_Key_KeyId",
                table: "UnsignedDocuments",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "KeyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignedDocuments_Key_KeyId",
                table: "SignedDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsignedDocuments_Key_KeyId",
                table: "UnsignedDocuments");

            migrationBuilder.DropIndex(
                name: "IX_UnsignedDocuments_KeyId",
                table: "UnsignedDocuments");

            migrationBuilder.DropIndex(
                name: "IX_SignedDocuments_KeyId",
                table: "SignedDocuments");

            migrationBuilder.DropColumn(
                name: "KeyId",
                table: "UnsignedDocuments");

            migrationBuilder.DropColumn(
                name: "KeyId",
                table: "SignedDocuments");
        }
    }
}
