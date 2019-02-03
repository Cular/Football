using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class ALTER_MeetTime_Add_IsChoosen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ischoosen",
                table: "meetingtimes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ischoosen",
                table: "meetingtimes");
        }
    }
}
