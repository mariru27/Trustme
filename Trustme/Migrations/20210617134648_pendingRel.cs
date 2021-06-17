using Microsoft.EntityFrameworkCore.Migrations;

namespace Trustme.Migrations
{
    public partial class pendingRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pending_User_UserId",
                table: "Pending");

            migrationBuilder.DropIndex(
                name: "IX_Pending_UserId",
                table: "Pending");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pending");

            migrationBuilder.AlterColumn<int>(
                name: "IdPedingUsers",
                table: "Pending",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pending_User_IdPedingUsers",
                table: "Pending",
                column: "IdPedingUsers",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pending_User_IdPedingUsers",
                table: "Pending");

            migrationBuilder.AlterColumn<int>(
                name: "IdPedingUsers",
                table: "Pending",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pending",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pending_UserId",
                table: "Pending",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pending_User_UserId",
                table: "Pending",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
