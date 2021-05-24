using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class changednameofatributeinOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "CellebrationTypeId",
                table: "BaseUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_CellebrationTypeId",
                table: "BaseUsers",
                column: "CellebrationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CellebrationTypeId",
                table: "BaseUsers",
                column: "CellebrationTypeId",
                principalTable: "CellebrationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CellebrationTypeId",
                table: "BaseUsers");

            migrationBuilder.DropIndex(
                name: "IX_BaseUsers_CellebrationTypeId",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "CellebrationTypeId",
                table: "BaseUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "CelebrationTypeId",
                table: "BaseUsers",
                type: "char(36)",
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
    }
}
