using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    public partial class FillColumnIsActiveInEventsWithValuesTrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            update Events
            set IsActive = 'true'
            where IsActive IS NULL;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
