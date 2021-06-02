using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class renamedatr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseNotificationType",
                table: "ResponseNotifications");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ResponseNotifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ResponseNotifications");

            migrationBuilder.AddColumn<int>(
                name: "ResponseNotificationType",
                table: "ResponseNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
