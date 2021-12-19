using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class AddedTableWithDrawingResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrawingResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GivingPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_DrawingResults_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_EventId",
                table: "DrawingResults",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_PersonId",
                table: "DrawingResults",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Persons_PersonsId",
                table: "EventPerson");

            migrationBuilder.DropTable(
                name: "DrawingResults");

            migrationBuilder.RenameColumn(
                name: "PersonsId",
                table: "EventPerson",
                newName: "Ids");

            migrationBuilder.RenameIndex(
                name: "IX_EventPerson_PersonsId",
                table: "EventPerson",
                newName: "IX_EventPerson_Ids");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Persons_Ids",
                table: "EventPerson",
                column: "Ids",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
