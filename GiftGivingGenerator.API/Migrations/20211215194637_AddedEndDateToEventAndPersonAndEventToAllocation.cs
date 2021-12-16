using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class AddedEndDateToEventAndPersonAndEventToAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Events_PersonEventsId",
                table: "EventPerson");

            migrationBuilder.RenameColumn(
                name: "PersonEventsId",
                table: "EventPerson",
                newName: "EventsId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Events_EventsId",
                table: "EventPerson",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Events_EventsId",
                table: "EventPerson");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "EventPerson",
                newName: "PersonEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Events_PersonEventsId",
                table: "EventPerson",
                column: "PersonEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
