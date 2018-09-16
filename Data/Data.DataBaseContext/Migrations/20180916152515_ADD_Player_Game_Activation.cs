using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class ADD_Player_Game_Activation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    passwordhash = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    adminid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.id);
                    table.ForeignKey(
                        name: "FK_games_players_adminid",
                        column: x => x.adminid,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "playeractivations",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    playerid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playeractivations", x => x.id);
                    table.ForeignKey(
                        name: "FK_playeractivations_players_playerid",
                        column: x => x.playerid,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_adminid",
                table: "games",
                column: "adminid");

            migrationBuilder.CreateIndex(
                name: "IX_playeractivations_playerid",
                table: "playeractivations",
                column: "playerid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_players_email",
                table: "players",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "playeractivations");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
