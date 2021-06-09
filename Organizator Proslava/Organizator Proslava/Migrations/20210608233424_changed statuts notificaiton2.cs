using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class changedstatutsnotificaiton2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewCommentNotification_ProposalId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NewCommentNotification_ProposalId",
                table: "Notifications",
                column: "NewCommentNotification_ProposalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CelebrationProposals_NewCommentNotification_Pr~",
                table: "Notifications",
                column: "NewCommentNotification_ProposalId",
                principalTable: "CelebrationProposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CelebrationProposals_NewCommentNotification_Pr~",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NewCommentNotification_ProposalId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NewCommentNotification_ProposalId",
                table: "Notifications");
        }
    }
}
