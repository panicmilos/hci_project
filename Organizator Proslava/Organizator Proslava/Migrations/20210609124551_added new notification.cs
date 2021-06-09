using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class addednewnotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CanceledResponseNotification_CelebrationType",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Client",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CanceledResponseNotification_CelebrationType",
                table: "Notifications");
        }
    }
}
