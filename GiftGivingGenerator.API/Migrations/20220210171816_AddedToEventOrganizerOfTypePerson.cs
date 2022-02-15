using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class AddedToEventOrganizerOfTypePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Organizer2Id",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@"
                                    insert into Persons (Id, Name, Email)
                                        select Id, Name, Email
                                        from Organizer
                                    update Events
                                    set Organizer2Id = OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Organizer2Id",
                table: "Events",
                column: "Organizer2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_Organizer2Id",
                table: "Events",
                column: "Organizer2Id",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_Organizer2Id",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Organizer2Id",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Organizer2Id",
                table: "Events");
        }
    }
}
