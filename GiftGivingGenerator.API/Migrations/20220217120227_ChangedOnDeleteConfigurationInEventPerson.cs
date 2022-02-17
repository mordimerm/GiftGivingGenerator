using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class ChangedOnDeleteConfigurationInEventPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPerson_Persons_PersonId",
                table: "EventPerson");

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
                name: "FK_EventPerson_Persons_PersonId",
                table: "EventPerson");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPerson_Persons_PersonId",
                table: "EventPerson",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
