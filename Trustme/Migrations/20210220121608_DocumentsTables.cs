using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class DocumentsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SignedDocumentId",
                table: "Key",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SignedDocuments",
                columns: table => new
                {
                    IdSignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<byte[]>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignedDocuments", x => x.IdSignedDocument);
                });

            migrationBuilder.CreateTable(
                name: "UnsignedDocuments",
                columns: table => new
                {
                    IdUnisignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<byte[]>(nullable: true),
                    KeyPreference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsignedDocuments", x => x.IdUnisignedDocument);
                });

            migrationBuilder.CreateTable(
                name: "UserSignedDocuments",
                columns: table => new
                {
                    IdUserSignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    SignedDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignedDocuments", x => x.IdUserSignedDocument);
                    table.ForeignKey(
                        name: "FK_UserSignedDocuments_SignedDocuments_SignedDocumentId",
                        column: x => x.SignedDocumentId,
                        principalTable: "SignedDocuments",
                        principalColumn: "IdSignedDocument",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSignedDocuments_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUnsignedDocuments",
                columns: table => new
                {
                    IdUserUnsignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UnsignedDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnsignedDocuments", x => x.IdUserUnsignedDocument);
                    table.ForeignKey(
                        name: "FK_UserUnsignedDocuments_UnsignedDocuments_UnsignedDocumentId",
                        column: x => x.UnsignedDocumentId,
                        principalTable: "UnsignedDocuments",
                        principalColumn: "IdUnisignedDocument",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnsignedDocuments_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Key_SignedDocumentId",
                table: "Key",
                column: "SignedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSignedDocuments_SignedDocumentId",
                table: "UserSignedDocuments",
                column: "SignedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSignedDocuments_UserId",
                table: "UserSignedDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnsignedDocuments_UnsignedDocumentId",
                table: "UserUnsignedDocuments",
                column: "UnsignedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnsignedDocuments_UserId",
                table: "UserUnsignedDocuments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Key_SignedDocuments_SignedDocumentId",
                table: "Key",
                column: "SignedDocumentId",
                principalTable: "SignedDocuments",
                principalColumn: "IdSignedDocument",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Key_SignedDocuments_SignedDocumentId",
                table: "Key");

            migrationBuilder.DropTable(
                name: "UserSignedDocuments");

            migrationBuilder.DropTable(
                name: "UserUnsignedDocuments");

            migrationBuilder.DropTable(
                name: "SignedDocuments");

            migrationBuilder.DropTable(
                name: "UnsignedDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Key_SignedDocumentId",
                table: "Key");

            migrationBuilder.DropColumn(
                name: "SignedDocumentId",
                table: "Key");
        }
    }
}
