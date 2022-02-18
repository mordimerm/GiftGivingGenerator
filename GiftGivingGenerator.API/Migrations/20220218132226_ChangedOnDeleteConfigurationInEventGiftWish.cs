using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class ChangedOnDeleteConfigurationInEventGiftWish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftWish_Events_EventId",
                table: "GiftWish");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftWish_Events_EventId",
                table: "GiftWish",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftWish_Events_EventId",
                table: "GiftWish");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftWish_Events_EventId",
                table: "GiftWish",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
