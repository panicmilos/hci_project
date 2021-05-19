using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedsomenewattributestocollaborators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "PlaceableEntities",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Movable",
                table: "PlaceableEntities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorId",
                table: "CelebrationHalls",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationHalls_CollaboratorId",
                table: "CelebrationHalls",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls",
                column: "CollaboratorId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                table: "CelebrationHalls");

            migrationBuilder.DropIndex(
                name: "IX_CelebrationHalls_CollaboratorId",
                table: "CelebrationHalls");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "PlaceableEntities");

            migrationBuilder.DropColumn(
                name: "Movable",
                table: "PlaceableEntities");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "CelebrationHalls");
        }
    }
}
