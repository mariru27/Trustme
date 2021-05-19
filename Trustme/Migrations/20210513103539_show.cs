using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class show : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcceptedPending");

            migrationBuilder.DropTable(
                name: "PendingRequest");

            migrationBuilder.AddColumn<bool>(
                name: "Show",
                table: "UnsignedDocument",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Pending",
                columns: table => new
                {
                    IdPedingUsers = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsernameWhoSentPending = table.Column<string>(nullable: true),
                    Accepted = table.Column<bool>(nullable: false),
                    TimeSentPendingRequest = table.Column<DateTime>(nullable: false),
                    TimeAcceptedPendingRequest = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pending", x => x.IdPedingUsers);
                    table.ForeignKey(
                        name: "FK_Pending_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pending_UserId",
                table: "Pending",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pending");

            migrationBuilder.DropColumn(
                name: "Show",
                table: "UnsignedDocument");

            migrationBuilder.CreateTable(
                name: "AcceptedPending",
                columns: table => new
                {
                    IdAcceptedPending = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeAcceptedPending = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    IdPedingUsers = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimePendingRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
