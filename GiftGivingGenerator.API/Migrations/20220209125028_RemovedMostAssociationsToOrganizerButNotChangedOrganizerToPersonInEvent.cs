using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class RemovedMostAssociationsToOrganizerButNotChangedOrganizerToPersonInEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Organizer_OrganizerId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_OrganizerId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Persons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Persons",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizerId",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Persons_OrganizerId",
                table: "Persons",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Organizer_OrganizerId",
                table: "Persons",
                column: "OrganizerId",
                principalTable: "Organizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
