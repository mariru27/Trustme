using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class deleteFk_SignedDocument_fomKeyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Key_SignedDocuments_SignedDocumentId",
                table: "Key");

            migrationBuilder.DropIndex(
                name: "IX_Key_SignedDocumentId",
                table: "Key");

            migrationBuilder.DropColumn(
                name: "SignedDocumentId",
                table: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SignedDocumentId",
                table: "Key",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Key_SignedDocumentId",
                table: "Key",
                column: "SignedDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Key_SignedDocuments_SignedDocumentId",
                table: "Key",
                column: "SignedDocumentId",
                principalTable: "SignedDocuments",
                principalColumn: "IdSignedDocument",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
