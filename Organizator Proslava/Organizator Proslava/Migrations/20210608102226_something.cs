using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Address_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Address_Organizer_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CellebrationTypeId",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "NumberOfGuests",
                table: "CelebrationHalls");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_Address_AddressId",
                table: "BaseUsers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_Address_Organizer_AddressId",
                table: "BaseUsers",
                column: "Organizer_AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CellebrationTypeId",
                table: "BaseUsers",
                column: "CellebrationTypeId",
                principalTable: "CellebrationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Address_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Address_Organizer_AddressId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CellebrationTypeId",
                table: "BaseUsers");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfGuests",
                table: "CelebrationHalls",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_CellebrationTypes_CellebrationTypeId",
                table: "BaseUsers",
                column: "CellebrationTypeId",
                principalTable: "CellebrationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
