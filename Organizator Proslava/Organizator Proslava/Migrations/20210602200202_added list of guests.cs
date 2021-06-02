using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedlistofguests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PositionX = table.Column<double>(nullable: false),
                    PositionY = table.Column<double>(nullable: false),
                    DinningTableId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guest_PlaceableEntities_DinningTableId",
                        column: x => x.DinningTableId,
                        principalTable: "PlaceableEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CelebrationResponseId",
                table: "Notifications",
                column: "CelebrationResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Guest_DinningTableId",
                table: "Guest",
                column: "DinningTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CelebrationResponses_CelebrationResponseId",
                table: "Notifications",
                column: "CelebrationResponseId",
                principalTable: "CelebrationResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CelebrationResponses_CelebrationResponseId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CelebrationResponseId",
                table: "Notifications");
        }
    }
}
