using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class ChangedNameFromOrganizer2ToOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_Organizer2Id",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Organizer2Id",
                table: "Events",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_Organizer2Id",
                table: "Events",
                newName: "IX_Events_OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_OrganizerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Events",
                newName: "Organizer2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                newName: "IX_Events_Organizer2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_Organizer2Id",
                table: "Events",
                column: "Organizer2Id",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
