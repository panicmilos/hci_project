using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class refactorednotificationsmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponseNotifications");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ForUserId = table.Column<Guid>(nullable: false),
                    CelebrationResponseId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ProposalId = table.Column<Guid>(nullable: true),
                    NumOfComments = table.Column<int>(nullable: true),
                    DetailId = table.Column<Guid>(nullable: true),
                    NewProposalNotification_ProposalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_CelebrationProposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "CelebrationProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_CelebrationDetail_DetailId",
                        column: x => x.DetailId,
                        principalTable: "CelebrationDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_CelebrationProposals_NewProposalNotification_P~",
                        column: x => x.NewProposalNotification_ProposalId,
                        principalTable: "CelebrationProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProposalId",
                table: "Notifications",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DetailId",
                table: "Notifications",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NewProposalNotification_ProposalId",
                table: "Notifications",
                column: "NewProposalNotification_ProposalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.CreateTable(
                name: "ResponseNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CelebrationResponseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ForObjectId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ForUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseNotifications", x => x.Id);
                });
        }
    }
}
