using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedcanceledproposalnotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CelebrationResponses_CelebrationResponseId",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationResponseId",
                table: "Notifications",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<string>(
                name: "CelebrationType",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organizer",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CelebrationResponses_CelebrationResponseId",
                table: "Notifications",
                column: "CelebrationResponseId",
                principalTable: "CelebrationResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CelebrationResponses_CelebrationResponseId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CelebrationType",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Organizer",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationResponseId",
                table: "Notifications",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CelebrationResponses_CelebrationResponseId",
                table: "Notifications",
                column: "CelebrationResponseId",
                principalTable: "CelebrationResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
