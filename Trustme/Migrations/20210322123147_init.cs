using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    IdRole = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    SecondName = table.Column<string>(nullable: false),
                    Mail = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserKey",
                columns: table => new
                {
                    IdUserKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKey", x => x.IdUserKey);
                    table.ForeignKey(
                        name: "FK_UserKey_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Key",
                columns: table => new
                {
                    KeyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PublicKey = table.Column<string>(nullable: true),
                    KeySize = table.Column<int>(nullable: false),
                    UserKeyIdUserKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Key", x => x.KeyId);
                    table.ForeignKey(
                        name: "FK_Key_UserKey_UserKeyIdUserKey",
                        column: x => x.UserKeyIdUserKey,
                        principalTable: "UserKey",
                        principalColumn: "IdUserKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SignedDocument",
                columns: table => new
                {
                    IdSignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SentFromUsername = table.Column<string>(nullable: true),
                    SignedByUsername = table.Column<string>(nullable: true),
                    Document = table.Column<byte[]>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    KeyId = table.Column<int>(nullable: false),
                    SignedTime = table.Column<DateTime>(nullable: false),
                    SentTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignedDocument", x => x.IdSignedDocument);
                    table.ForeignKey(
                        name: "FK_SignedDocument_Key_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Key",
                        principalColumn: "KeyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnsignedDocument",
                columns: table => new
                {
                    IdUnsignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SentFromUsername = table.Column<string>(nullable: true),
                    Document = table.Column<byte[]>(nullable: true),
                    KeyPreference = table.Column<string>(nullable: true),
                    Signed = table.Column<bool>(nullable: false),
                    KeyId = table.Column<int>(nullable: false),
                    SentTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsignedDocument", x => x.IdUnsignedDocument);
                    table.ForeignKey(
                        name: "FK_UnsignedDocument_Key_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Key",
                        principalColumn: "KeyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSignedDocument",
                columns: table => new
                {
                    IdUserSignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    SignedDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignedDocument", x => x.IdUserSignedDocument);
                    table.ForeignKey(
                        name: "FK_UserSignedDocument_SignedDocument_SignedDocumentId",
                        column: x => x.SignedDocumentId,
                        principalTable: "SignedDocument",
                        principalColumn: "IdSignedDocument",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSignedDocument_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUnsignedDocument",
                columns: table => new
                {
                    IdUserUnsignedDocument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UnsignedDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnsignedDocument", x => x.IdUserUnsignedDocument);
                    table.ForeignKey(
                        name: "FK_UserUnsignedDocument_UnsignedDocument_UnsignedDocumentId",
                        column: x => x.UnsignedDocumentId,
                        principalTable: "UnsignedDocument",
                        principalColumn: "IdUnsignedDocument",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnsignedDocument_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Key_UserKeyIdUserKey",
                table: "Key",
                column: "UserKeyIdUserKey");

            migrationBuilder.CreateIndex(
                name: "IX_SignedDocument_KeyId",
                table: "SignedDocument",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsignedDocument_KeyId",
                table: "UnsignedDocument",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKey_UserId",
                table: "UserKey",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSignedDocument_SignedDocumentId",
                table: "UserSignedDocument",
                column: "SignedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSignedDocument_UserId",
                table: "UserSignedDocument",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnsignedDocument_UnsignedDocumentId",
                table: "UserUnsignedDocument",
                column: "UnsignedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnsignedDocument_UserId",
                table: "UserUnsignedDocument",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSignedDocument");

            migrationBuilder.DropTable(
                name: "UserUnsignedDocument");

            migrationBuilder.DropTable(
                name: "SignedDocument");

            migrationBuilder.DropTable(
                name: "UnsignedDocument");

            migrationBuilder.DropTable(
                name: "Key");

            migrationBuilder.DropTable(
                name: "UserKey");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
