using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class PlayerActivation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "playerid",
                table: "playeractivations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_playeractivations_playerid",
                table: "playeractivations",
                column: "playerid",
                unique: true,
                filter: "[playerid] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_playeractivations_players_playerid",
                table: "playeractivations",
                column: "playerid",
                principalTable: "players",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playeractivations_players_playerid",
                table: "playeractivations");

            migrationBuilder.DropIndex(
                name: "IX_playeractivations_playerid",
                table: "playeractivations");

            migrationBuilder.AlterColumn<string>(
                name: "playerid",
                table: "playeractivations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
