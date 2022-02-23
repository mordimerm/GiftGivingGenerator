using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class ChangedNameOfDBExclusionToExclusions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exclusion_Events_EventId",
                table: "Exclusion");

            migrationBuilder.DropForeignKey(
                name: "FK_Exclusion_Persons_ExcludedId",
                table: "Exclusion");

            migrationBuilder.DropForeignKey(
                name: "FK_Exclusion_Persons_PersonId",
                table: "Exclusion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exclusion",
                table: "Exclusion");

            migrationBuilder.RenameTable(
                name: "Exclusion",
                newName: "Exclusions");

            migrationBuilder.RenameIndex(
                name: "IX_Exclusion_PersonId",
                table: "Exclusions",
                newName: "IX_Exclusions_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Exclusion_ExcludedId",
                table: "Exclusions",
                newName: "IX_Exclusions_ExcludedId");

            migrationBuilder.RenameIndex(
                name: "IX_Exclusion_EventId",
                table: "Exclusions",
                newName: "IX_Exclusions_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exclusions",
                table: "Exclusions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusions_Events_EventId",
                table: "Exclusions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusions_Persons_ExcludedId",
                table: "Exclusions",
                column: "ExcludedId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Exclusions_Events_EventId",
                table: "Exclusions");

            migrationBuilder.DropForeignKey(
                name: "FK_Exclusions_Persons_ExcludedId",
                table: "Exclusions");

            migrationBuilder.DropForeignKey(
                name: "FK_Exclusions_Persons_PersonId",
                table: "Exclusions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exclusions",
                table: "Exclusions");

            migrationBuilder.RenameTable(
                name: "Exclusions",
                newName: "Exclusion");

            migrationBuilder.RenameIndex(
                name: "IX_Exclusions_PersonId",
                table: "Exclusion",
                newName: "IX_Exclusion_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Exclusions_ExcludedId",
                table: "Exclusion",
                newName: "IX_Exclusion_ExcludedId");

            migrationBuilder.RenameIndex(
                name: "IX_Exclusions_EventId",
                table: "Exclusion",
                newName: "IX_Exclusion_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exclusion",
                table: "Exclusion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusion_Events_EventId",
                table: "Exclusion",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusion_Persons_ExcludedId",
                table: "Exclusion",
                column: "ExcludedId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusion_Persons_PersonId",
                table: "Exclusion",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
