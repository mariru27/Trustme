using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class unsigneddocument_edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUnsignedDocuments_UnsignedDocuments_UnsignedDocumentId",
                table: "UserUnsignedDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnsignedDocuments",
                table: "UnsignedDocuments");

            migrationBuilder.DropColumn(
                name: "IdUnisignedDocument",
                table: "UnsignedDocuments");

            migrationBuilder.AddColumn<int>(
                name: "IdUnsignedDocument",
                table: "UnsignedDocuments",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnsignedDocuments",
                table: "UnsignedDocuments",
                column: "IdUnsignedDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUnsignedDocuments_UnsignedDocuments_UnsignedDocumentId",
                table: "UserUnsignedDocuments",
                column: "UnsignedDocumentId",
                principalTable: "UnsignedDocuments",
                principalColumn: "IdUnsignedDocument",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUnsignedDocuments_UnsignedDocuments_UnsignedDocumentId",
                table: "UserUnsignedDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnsignedDocuments",
                table: "UnsignedDocuments");

            migrationBuilder.DropColumn(
                name: "IdUnsignedDocument",
                table: "UnsignedDocuments");

            migrationBuilder.AddColumn<int>(
                name: "IdUnisignedDocument",
                table: "UnsignedDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnsignedDocuments",
                table: "UnsignedDocuments",
                column: "IdUnisignedDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUnsignedDocuments_UnsignedDocuments_UnsignedDocumentId",
                table: "UserUnsignedDocuments",
                column: "UnsignedDocumentId",
                principalTable: "UnsignedDocuments",
                principalColumn: "IdUnisignedDocument",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
