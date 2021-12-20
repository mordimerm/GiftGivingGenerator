using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class AddedForeignKeyToDrawingResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_GiverPersonId",
                table: "DrawingResults",
                column: "GiverPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawingResults_RecipientPersonId",
                table: "DrawingResults",
                column: "RecipientPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrawingResults_Persons_GiverPersonId",
                table: "DrawingResults",
                column: "GiverPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrawingResults_Persons_RecipientPersonId",
                table: "DrawingResults",
                column: "RecipientPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrawingResults_Persons_GiverPersonId",
                table: "DrawingResults");

            migrationBuilder.DropForeignKey(
                name: "FK_DrawingResults_Persons_RecipientPersonId",
                table: "DrawingResults");

            migrationBuilder.DropIndex(
                name: "IX_DrawingResults_GiverPersonId",
                table: "DrawingResults");

            migrationBuilder.DropIndex(
                name: "IX_DrawingResults_RecipientPersonId",
                table: "DrawingResults");
        }
    }
}
