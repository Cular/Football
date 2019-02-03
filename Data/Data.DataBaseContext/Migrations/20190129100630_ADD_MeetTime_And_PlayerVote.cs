using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class ADD_MeetTime_And_PlayerVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meetingtimes",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    gameid = table.Column<Guid>(nullable: false),
                    timeofmeet = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meetingtimes", x => x.id);
                    table.ForeignKey(
                        name: "FK_meetingtimes_games_gameid",
                        column: x => x.gameid,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playersgames",
                columns: table => new
                {
                    playerid = table.Column<string>(nullable: false),
                    gameid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playersgames", x => new { x.playerid, x.gameid });
                    table.ForeignKey(
                        name: "FK_playersgames_games_gameid",
                        column: x => x.gameid,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_playersgames_players_playerid",
                        column: x => x.playerid,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playervotes",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    meetingtimeid = table.Column<Guid>(nullable: false),
                    playerid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playervotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_playervotes_meetingtimes_meetingtimeid",
                        column: x => x.meetingtimeid,
                        principalTable: "meetingtimes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meetingtimes_gameid",
                table: "meetingtimes",
                column: "gameid");

            migrationBuilder.CreateIndex(
                name: "IX_playersgames_gameid",
                table: "playersgames",
                column: "gameid");

            migrationBuilder.CreateIndex(
                name: "IX_playervotes_meetingtimeid",
                table: "playervotes",
                column: "meetingtimeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playersgames");

            migrationBuilder.DropTable(
                name: "playervotes");

            migrationBuilder.DropTable(
                name: "meetingtimes");
        }
    }
}
