using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedservicetoproposall : Migration

    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ProposedServiceId",
                table: "CelebrationProposals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProposedService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    NumberOfService = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposedService", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationProposals_ProposedServiceId",
                table: "CelebrationProposals",
                column: "ProposedServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationProposals_ProposedService_ProposedServiceId",
                table: "CelebrationProposals",
                column: "ProposedServiceId",
                principalTable: "ProposedService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationProposals_ProposedService_ProposedServiceId",
                table: "CelebrationProposals");

            migrationBuilder.DropTable(
                name: "ProposedService");

            migrationBuilder.DropIndex(
                name: "IX_CelebrationProposals_ProposedServiceId",
                table: "CelebrationProposals");

            migrationBuilder.DropColumn(
                name: "ProposedServiceId",
                table: "CelebrationProposals");

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorServiceId",
                table: "CelebrationProposals",
                type: "char(36)",
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
    }
}