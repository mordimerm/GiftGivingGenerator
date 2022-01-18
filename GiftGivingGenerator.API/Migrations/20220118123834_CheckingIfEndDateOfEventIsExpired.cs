using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class CheckingIfEndDateOfEventIsExpired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Events",
                type: "bit",
                nullable: true,
                computedColumnSql: "IIF(EndDate < GetUtcDate(), 'false', 'true')",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Events",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComputedColumnSql: "IIF(EndDate < GetUtcDate(), 'false', 'true')");
        }
    }
}
