using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtraForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exclusions_Persons_PersonId1",
                table: "Exclusions");

            migrationBuilder.DropIndex(
                name: "IX_Exclusions_PersonId1",
                table: "Exclusions");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Exclusions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonId1",
                table: "Exclusions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_PersonId1",
                table: "Exclusions",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Exclusions_Persons_PersonId1",
                table: "Exclusions",
                column: "PersonId1",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
