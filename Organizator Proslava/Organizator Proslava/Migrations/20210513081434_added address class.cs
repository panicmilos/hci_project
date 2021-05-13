using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addedaddressclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "Organizer_Address",
                table: "BaseUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "BaseUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Organizer_AddressId",
                table: "BaseUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    WholeAddress = table.Column<string>(nullable: true),
                    Lat = table.Column<float>(nullable: false),
                    Lng = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_AddressId",
                table: "BaseUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_Organizer_AddressId",
                table: "BaseUsers",
                column: "Organizer_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_Address_AddressId",
                table: "BaseUsers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_Address_Organizer_AddressId",
                table: "BaseUsers",
                column: "Organizer_AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Address_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Address_Organizer_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_BaseUsers_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropIndex(
                name: "IX_BaseUsers_Organizer_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "Organizer_AddressId",
                table: "BaseUsers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BaseUsers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organizer_Address",
                table: "BaseUsers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
