using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class ALTER_MeetingTime_Fixed_IsChosen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ischoosen",
                table: "meetingtimes",
                newName: "ischosen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ischosen",
                table: "meetingtimes",
                newName: "ischoosen");
        }
    }
}
