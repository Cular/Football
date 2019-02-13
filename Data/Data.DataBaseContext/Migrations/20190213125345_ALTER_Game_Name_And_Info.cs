using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class ALTER_Game_Name_And_Info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "info",
                table: "games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "games",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "info",
                table: "games");

            migrationBuilder.DropColumn(
                name: "name",
                table: "games");
        }
    }
}
