using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class AddedListOfEventsToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Events_EventId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_EventId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "EventPerson",
                columns: table => new
                {
                    PersonEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPerson", x => new { x.PersonEventsId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_EventPerson_Events_PersonEventsId",
                        column: x => x.PersonEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPerson_PersonsId",
                table: "EventPerson",
                column: "Ids");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventPerson");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EventId",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Events_EventId",
                table: "Persons",
                column: "Id",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
