using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class ActivationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "playeractivations",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    playerid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playeractivations", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playeractivations");

            migrationBuilder.DropColumn(
                name: "active",
                table: "players");
        }
    }
}
