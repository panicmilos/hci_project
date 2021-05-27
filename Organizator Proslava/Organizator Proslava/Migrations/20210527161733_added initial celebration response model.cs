using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedinitialcelebrationresponsemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Celebrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    OrganizerId = table.Column<Guid>(nullable: true),
                    AddressId = table.Column<Guid>(nullable: true),
                    DateTimeFrom = table.Column<DateTime>(nullable: false),
                    DateTimeTo = table.Column<DateTime>(nullable: false),
                    ExpectedNumberOfGuests = table.Column<int>(nullable: false),
                    BudgetFrom = table.Column<float>(nullable: false),
                    BudgetTo = table.Column<float>(nullable: false),
                    IsBudgetFixed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celebrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Celebrations_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Celebrations_BaseUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Celebrations_BaseUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "BaseUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CelebrationId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CelebrationDetail_Celebrations_CelebrationId",
                        column: x => x.CelebrationId,
                        principalTable: "Celebrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CelebrationId = table.Column<Guid>(nullable: false),
                    OrganizerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrationResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CelebrationResponses_Celebrations_CelebrationId",
                        column: x => x.CelebrationId,
                        principalTable: "Celebrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CelebrationResponses_BaseUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "BaseUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationProposals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CelebrationReponseId = table.Column<Guid>(nullable: false),
                    CelebrationResponseId = table.Column<Guid>(nullable: true),
                    CelebrationDetailId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrationProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CelebrationProposals_CelebrationDetail_CelebrationDetailId",
                        column: x => x.CelebrationDetailId,
                        principalTable: "CelebrationDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CelebrationProposals_CelebrationResponses_CelebrationRespons~",
                        column: x => x.CelebrationResponseId,
                        principalTable: "CelebrationResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationDetail_CelebrationId",
                table: "CelebrationDetail",
                column: "CelebrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationProposals_CelebrationDetailId",
                table: "CelebrationProposals",
                column: "CelebrationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationProposals_CelebrationResponseId",
                table: "CelebrationProposals",
                column: "CelebrationResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationResponses_CelebrationId",
                table: "CelebrationResponses",
                column: "CelebrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationResponses_OrganizerId",
                table: "CelebrationResponses",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Celebrations_AddressId",
                table: "Celebrations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Celebrations_ClientId",
                table: "Celebrations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Celebrations_OrganizerId",
                table: "Celebrations",
                column: "OrganizerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CelebrationProposals");

            migrationBuilder.DropTable(
                name: "CelebrationDetail");

            migrationBuilder.DropTable(
                name: "CelebrationResponses");

            migrationBuilder.DropTable(
                name: "Celebrations");
        }
    }
}
