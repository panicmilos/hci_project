using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organizator_Proslava.Migrations
{
    public partial class newinitialmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "CellebrationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellebrationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Collaborator_PhoneNumber = table.Column<string>(nullable: true),
                    AddressId = table.Column<Guid>(nullable: true),
                    Images = table.Column<string>(nullable: true),
                    PersonalId = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    IdentificationNumber = table.Column<string>(nullable: true),
                    PIB = table.Column<string>(nullable: true),
                    Organizer_PersonalId = table.Column<string>(nullable: true),
                    Organizer_JMBG = table.Column<string>(nullable: true),
                    Organizer_PhoneNumber = table.Column<string>(nullable: true),
                    Organizer_AddressId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseUsers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseUsers_Address_Organizer_AddressId",
                        column: x => x.Organizer_AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationHalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumberOfGuests = table.Column<int>(nullable: false),
                    CollaboratorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrationHalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CelebrationHalls_BaseUsers_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "BaseUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorServiceBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CollaboratorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorServiceBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorServiceBooks_BaseUsers_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "BaseUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceableEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PositionX = table.Column<double>(nullable: false),
                    PositionY = table.Column<double>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Movable = table.Column<bool>(nullable: false),
                    CelebrationHallId = table.Column<Guid>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Seats = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceableEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceableEntities_CelebrationHalls_CelebrationHallId",
                        column: x => x.CelebrationHallId,
                        principalTable: "CelebrationHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    CollaboratorServiceBookId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorServices_CollaboratorServiceBooks_CollaboratorSe~",
                        column: x => x.CollaboratorServiceBookId,
                        principalTable: "CollaboratorServiceBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_AddressId",
                table: "BaseUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUsers_Organizer_AddressId",
                table: "BaseUsers",
                column: "Organizer_AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationHalls_CollaboratorId",
                table: "CelebrationHalls",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorServiceBooks_CollaboratorId",
                table: "CollaboratorServiceBooks",
                column: "CollaboratorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorServices_CollaboratorServiceBookId",
                table: "CollaboratorServices",
                column: "CollaboratorServiceBookId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceableEntities_CelebrationHallId",
                table: "PlaceableEntities",
                column: "CelebrationHallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CellebrationTypes");

            migrationBuilder.DropTable(
                name: "CollaboratorServices");

            migrationBuilder.DropTable(
                name: "PlaceableEntities");

            migrationBuilder.DropTable(
                name: "CollaboratorServiceBooks");

            migrationBuilder.DropTable(
                name: "CelebrationHalls");

            migrationBuilder.DropTable(
                name: "BaseUsers");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
