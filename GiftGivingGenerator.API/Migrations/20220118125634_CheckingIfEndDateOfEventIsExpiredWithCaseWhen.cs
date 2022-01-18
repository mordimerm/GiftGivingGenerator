using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class CheckingIfEndDateOfEventIsExpiredWithCaseWhen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Events",
                type: "bit",
                nullable: true,
                computedColumnSql: "CASE WHEN EndDate < GETUTCDATE() THEN 'false' ELSE 'true' END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComputedColumnSql: "IIF(EndDate < GetUtcDate(), 'false', 'true')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Events",
                type: "bit",
                nullable: true,
                computedColumnSql: "IIF(EndDate < GetUtcDate(), 'false', 'true')",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComputedColumnSql: "CASE WHEN EndDate < GETUTCDATE() THEN 'false' ELSE 'true' END");
        }
    }
}
