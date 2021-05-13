using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedplaceableentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaceableEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PositionX = table.Column<int>(nullable: false),
                    PositionY = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceableEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceableEntities");
        }
    }
}
