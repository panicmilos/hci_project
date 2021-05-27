using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedproposalcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProposalComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    WriterId = table.Column<Guid>(nullable: false),
                    CelebrationProposalId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalComments_CelebrationProposals_CelebrationProposalId",
                        column: x => x.CelebrationProposalId,
                        principalTable: "CelebrationProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposalComments_BaseUsers_WriterId",
                        column: x => x.WriterId,
                        principalTable: "BaseUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProposalComments_CelebrationProposalId",
                table: "ProposalComments",
                column: "CelebrationProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalComments_WriterId",
                table: "ProposalComments",
                column: "WriterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProposalComments");
        }
    }
}
