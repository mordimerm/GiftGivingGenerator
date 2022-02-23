using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class AddedListOfExclusionsToPersonAndRemovedOneConfigurationInPresonExclusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exclusions_Persons_PersonId",
                table: "Exclusions");

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusions_Persons_PersonId",
                table: "Exclusions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exclusions_Persons_PersonId",
                table: "Exclusions");

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusions_Persons_PersonId",
                table: "Exclusions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
