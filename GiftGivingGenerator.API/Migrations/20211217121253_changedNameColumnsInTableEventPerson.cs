using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class changedNameColumnsInTableEventPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Events_EventsId",
                table: "EventPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Persons_PersonsId",
                table: "EventPerson");

            migrationBuilder.RenameColumn(
                name: "PersonsId",
                table: "EventPerson",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "EventPerson",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventPerson_PersonsId",
                table: "EventPerson",
                newName: "IX_EventPerson_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Events_EventId",
                table: "EventPerson",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Persons_PersonId",
                table: "EventPerson",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Events_EventId",
                table: "EventPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Persons_PersonId",
                table: "EventPerson");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "EventPerson",
                newName: "PersonsId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventPerson",
                newName: "EventsId");

            migrationBuilder.RenameIndex(
                name: "IX_EventPerson_PersonId",
                table: "EventPerson",
                newName: "IX_EventPerson_PersonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Events_EventsId",
                table: "EventPerson",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Persons_PersonsId",
                table: "EventPerson",
                column: "PersonsId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
