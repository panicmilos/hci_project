using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedsomenewpropertiestocelebrationproposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CelebrationHallId",
                table: "CelebrationProposals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorId",
                table: "CelebrationProposals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CelebrationProposals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationProposals_CelebrationHallId",
                table: "CelebrationProposals",
                column: "CelebrationHallId");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationProposals_CollaboratorId",
                table: "CelebrationProposals",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_CelebrationHalls_CelebrationHallId",
                table: "CelebrationProposals",
                column: "CelebrationHallId",
                principalTable: "CelebrationHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_BaseUsers_CollaboratorId",
                table: "CelebrationProposals",
                column: "CollaboratorId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_CelebrationHalls_CelebrationHallId",
                table: "CelebrationProposals");

            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_BaseUsers_CollaboratorId",
                table: "CelebrationProposals");

            migrationBuilder.DropIndex(
                name: "IX_CelebrationProposals_CelebrationHallId",
                table: "CelebrationProposals");

            migrationBuilder.DropIndex(
                name: "IX_CelebrationProposals_CollaboratorId",
                table: "CelebrationProposals");

            migrationBuilder.DropColumn(
                name: "CelebrationHallId",
                table: "CelebrationProposals");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "CelebrationProposals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CelebrationProposals");
        }
    }
}
