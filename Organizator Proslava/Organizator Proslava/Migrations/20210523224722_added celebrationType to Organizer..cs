using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedcelebrationTypetoOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CelebrationTypeId",
                table: "BaseUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_CelebrationTypeId",
                table: "BaseUsers",
                column: "CelebrationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CelebrationTypeId",
                table: "BaseUsers",
                column: "CelebrationTypeId",
                principalTable: "CellebrationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CelebrationTypeId",
                table: "BaseUsers");

            migrationBuilder.DropIndex(
                name: "IX_BaseUsers_CelebrationTypeId",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "CelebrationTypeId",
                table: "BaseUsers");
        }
    }
}
