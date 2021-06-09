using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedservicetoproposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorServiceId",
                table: "CelebrationProposals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationProposals_CollaboratorServiceId",
                table: "CelebrationProposals",
                column: "CollaboratorServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_CollaboratorServices_CollaboratorServic~",
                table: "CelebrationProposals",
                column: "CollaboratorServiceId",
                principalTable: "CollaboratorServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_CollaboratorServices_CollaboratorServic~",
                table: "CelebrationProposals");

            migrationBuilder.DropIndex(
                name: "IX_CelebrationProposals_CollaboratorServiceId",
                table: "CelebrationProposals");

            migrationBuilder.DropColumn(
                name: "CollaboratorServiceId",
                table: "CelebrationProposals");
        }
    }
}
