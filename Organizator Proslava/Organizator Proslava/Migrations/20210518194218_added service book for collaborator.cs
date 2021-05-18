using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedservicebookforcollaborator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorServiceBookId",
                table: "BaseUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CollaboratorServiceBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorServiceBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    CollaboratorServiceBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorServices_CollaboratorServiceBooks_CollaboratorSe~",
                        column: x => x.CollaboratorServiceBookId,
                        principalTable: "CollaboratorServiceBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_CollaboratorServiceBookId",
                table: "BaseUsers",
                column: "CollaboratorServiceBookId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorServices_CollaboratorServiceBookId",
                table: "CollaboratorServices",
                column: "CollaboratorServiceBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_CollaboratorServiceBooks_CollaboratorServiceBookId",
                table: "BaseUsers",
                column: "CollaboratorServiceBookId",
                principalTable: "CollaboratorServiceBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_CollaboratorServiceBooks_CollaboratorServiceBookId",
                table: "BaseUsers");

            migrationBuilder.DropTable(
                name: "CollaboratorServices");

            migrationBuilder.DropTable(
                name: "CollaboratorServiceBooks");

            migrationBuilder.DropIndex(
                name: "IX_BaseUsers_CollaboratorServiceBookId",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "CollaboratorServiceBookId",
                table: "BaseUsers");
        }
    }
}
