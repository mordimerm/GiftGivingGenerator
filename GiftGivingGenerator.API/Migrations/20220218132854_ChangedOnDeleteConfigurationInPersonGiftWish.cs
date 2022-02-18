using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class ChangedOnDeleteConfigurationInPersonGiftWish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftWish_Persons_PersonId",
                table: "GiftWish");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftWish_Persons_PersonId",
                table: "GiftWish",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftWish_Persons_PersonId",
                table: "GiftWish");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftWish_Persons_PersonId",
                table: "GiftWish",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
