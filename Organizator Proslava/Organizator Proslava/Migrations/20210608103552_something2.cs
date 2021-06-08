using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class something2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls",
                column: "CollaboratorId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls",
                column: "CollaboratorId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
