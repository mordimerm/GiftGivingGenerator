using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class RemovedPersonFromClassDrawingResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrawingResults_Persons_PersonId",
                table: "DrawingResults");

            migrationBuilder.DropIndex(
                name: "IX_DrawingResults_PersonId",
                table: "DrawingResults");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "DrawingResults");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "DrawingResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_PersonId",
                table: "DrawingResults",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrawingResults_Persons_PersonId",
                table: "DrawingResults",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
