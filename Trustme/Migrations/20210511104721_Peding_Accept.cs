using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class Peding_Accept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcceptedPending",
                columns: table => new
                {
                    IdAcceptedPending = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    TimeAcceptedPending = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptedPending", x => x.IdAcceptedPending);
                    table.ForeignKey(
                        name: "FK_AcceptedPending_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PendingRequest",
                columns: table => new
                {
                    IdPedingUsers = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    TimePendingRequest = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingRequest", x => x.IdPedingUsers);
                    table.ForeignKey(
                        name: "FK_PendingRequest_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcceptedPending_UserId",
                table: "AcceptedPending",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequest_UserId",
                table: "PendingRequest",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcceptedPending");

            migrationBuilder.DropTable(
                name: "PendingRequest");
        }
    }
}
