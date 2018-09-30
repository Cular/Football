using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DataBaseContext.Migrations
{
    public partial class ADD_RefreshToken_Fix_PlayerActivation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "refreshtokens",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    userid = table.Column<string>(nullable: false),
                    token = table.Column<string>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshtokens", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_refreshtokens_token",
                table: "refreshtokens",
                column: "token",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refreshtokens");
        }
    }
}
