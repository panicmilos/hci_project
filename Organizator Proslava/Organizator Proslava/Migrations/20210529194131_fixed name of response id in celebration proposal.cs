using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class fixednameofresponseidincelebrationproposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_CelebrationHalls_CelebrationHallId",
                table: "CelebrationProposals");

            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_CelebrationResponses_CelebrationRespons~",
                table: "CelebrationProposals");

            migrationBuilder.DropColumn(
                name: "CelebrationReponseId",
                table: "CelebrationProposals");

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationResponseId",
                table: "CelebrationProposals",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationHallId",
                table: "CelebrationProposals",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_CelebrationHalls_CelebrationHallId",
                table: "CelebrationProposals",
                column: "CelebrationHallId",
                principalTable: "CelebrationHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_CelebrationResponses_CelebrationRespons~",
                table: "CelebrationProposals",
                column: "CelebrationResponseId",
                principalTable: "CelebrationResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_CelebrationHalls_CelebrationHallId",
                table: "CelebrationProposals");

            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_CelebrationResponses_CelebrationRespons~",
                table: "CelebrationProposals");

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationResponseId",
                table: "CelebrationProposals",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationHallId",
                table: "CelebrationProposals",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CelebrationReponseId",
                table: "CelebrationProposals",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_CelebrationHalls_CelebrationHallId",
                table: "CelebrationProposals",
                column: "CelebrationHallId",
                principalTable: "CelebrationHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_CelebrationResponses_CelebrationRespons~",
                table: "CelebrationProposals",
                column: "CelebrationResponseId",
                principalTable: "CelebrationResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
