using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class nullableCollaboratorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorId",
                table: "CelebrationHalls",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls",
                column: "CollaboratorId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorId",
                table: "CelebrationHalls",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls",
                column: "CollaboratorId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
