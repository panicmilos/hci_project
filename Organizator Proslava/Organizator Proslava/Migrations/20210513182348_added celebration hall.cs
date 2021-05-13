using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedcelebrationhall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PositionY",
                table: "PlaceableEntities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "PositionX",
                table: "PlaceableEntities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "CelebrationHallId",
                table: "PlaceableEntities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CelebrationHalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumberOfGuests = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrationHalls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceableEntities_CelebrationHallId",
                table: "PlaceableEntities",
                column: "CelebrationHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceableEntities_CelebrationHalls_CelebrationHallId",
                table: "PlaceableEntities",
                column: "CelebrationHallId",
                principalTable: "CelebrationHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceableEntities_CelebrationHalls_CelebrationHallId",
                table: "PlaceableEntities");

            migrationBuilder.DropTable(
                name: "CelebrationHalls");

            migrationBuilder.DropIndex(
                name: "IX_PlaceableEntities_CelebrationHallId",
                table: "PlaceableEntities");

            migrationBuilder.DropColumn(
                name: "CelebrationHallId",
                table: "PlaceableEntities");

            migrationBuilder.AlterColumn<int>(
                name: "PositionY",
                table: "PlaceableEntities",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "PositionX",
                table: "PlaceableEntities",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
