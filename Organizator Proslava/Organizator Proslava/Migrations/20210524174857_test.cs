using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceableEntities_CelebrationHalls_CelebrationHallId",
                table: "PlaceableEntities");

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationHallId",
                table: "PlaceableEntities",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceableEntities_CelebrationHalls_CelebrationHallId",
                table: "PlaceableEntities",
                column: "CelebrationHallId",
                principalTable: "CelebrationHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceableEntities_CelebrationHalls_CelebrationHallId",
                table: "PlaceableEntities");

            migrationBuilder.AlterColumn<Guid>(
                name: "CelebrationHallId",
                table: "PlaceableEntities",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceableEntities_CelebrationHalls_CelebrationHallId",
                table: "PlaceableEntities",
                column: "CelebrationHallId",
                principalTable: "CelebrationHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
