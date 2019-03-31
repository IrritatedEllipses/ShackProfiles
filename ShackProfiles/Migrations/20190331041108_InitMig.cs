using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShackProfiles.Migrations
{
    public partial class InitMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShackProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Shackname = table.Column<string>(nullable: false),
                    DisplayShackname = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    TimeZone = table.Column<string>(nullable: true),
                    SteamName = table.Column<string>(nullable: true),
                    SteamUrl = table.Column<string>(nullable: true),
                    DiscordId = table.Column<string>(nullable: true),
                    PSN = table.Column<string>(nullable: true),
                    XboxGamertag = table.Column<string>(nullable: true),
                    NintendoId = table.Column<string>(nullable: true),
                    OriginId = table.Column<string>(nullable: true),
                    BattlenetId = table.Column<string>(nullable: true),
                    UplayId = table.Column<string>(nullable: true),
                    EpicGamesId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShackProfiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShackProfiles");
        }
    }
}
