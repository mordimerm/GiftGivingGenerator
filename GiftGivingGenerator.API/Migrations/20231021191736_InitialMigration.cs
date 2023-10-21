using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Budget = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Persons_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrawingResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiverPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawingResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrawingResults_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrawingResults_Persons_GiverPersonId",
                        column: x => x.GiverPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrawingResults_Persons_RecipientPersonId",
                        column: x => x.RecipientPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventPerson",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPerson", x => new { x.EventId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_EventPerson_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventPerson_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exclusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExcludedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exclusions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exclusions_Persons_ExcludedId",
                        column: x => x.ExcludedId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exclusions_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exclusions_Persons_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GiftWish",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftWish", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftWish_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiftWish_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_EventId",
                table: "DrawingResults",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_GiverPersonId",
                table: "DrawingResults",
                column: "GiverPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_RecipientPersonId",
                table: "DrawingResults",
                column: "RecipientPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPerson_PersonId",
                table: "EventPerson",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_EventId",
                table: "Exclusions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_ExcludedId",
                table: "Exclusions",
                column: "ExcludedId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_PersonId",
                table: "Exclusions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_PersonId1",
                table: "Exclusions",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_GiftWish_EventId",
                table: "GiftWish",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftWish_PersonId",
                table: "GiftWish",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawingResults");

            migrationBuilder.DropTable(
                name: "EventPerson");

            migrationBuilder.DropTable(
                name: "Exclusions");

            migrationBuilder.DropTable(
                name: "GiftWish");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
