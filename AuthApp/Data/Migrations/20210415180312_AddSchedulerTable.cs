using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApp.Data.Migrations
{
    public partial class AddSchedulerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    MalId = table.Column<long>(nullable: false),
                    RequestHash = table.Column<string>(nullable: true),
                    RequestCached = table.Column<bool>(nullable: false),
                    RequestCacheExpiry = table.Column<int>(nullable: false),
                    LinkCanonical = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    TitleEnglish = table.Column<string>(nullable: true),
                    TitleJapanese = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Episodes = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Airing = table.Column<bool>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: true),
                    ScoredBy = table.Column<int>(nullable: true),
                    Rank = table.Column<int>(nullable: true),
                    Popularity = table.Column<int>(nullable: true),
                    Members = table.Column<int>(nullable: true),
                    Favorites = table.Column<int>(nullable: true),
                    Synopsis = table.Column<string>(nullable: true),
                    Background = table.Column<string>(nullable: true),
                    Premiered = table.Column<string>(nullable: true),
                    Broadcast = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.MalId);
                });

            migrationBuilder.CreateTable(
                name: "MALSubItem",
                columns: table => new
                {
                    MalId = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MALSubItem", x => x.MalId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledAnimes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<long>(nullable: false),
                    AddedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledAnimes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "MalId", "Airing", "Background", "Broadcast", "Duration", "Episodes", "Favorites", "ImageURL", "LinkCanonical", "Members", "Popularity", "Premiered", "Rank", "Rating", "RequestCacheExpiry", "RequestCached", "RequestHash", "Score", "ScoredBy", "Source", "Status", "Synopsis", "Title", "TitleEnglish", "TitleJapanese", "Type" },
                values: new object[] { 8063L, false, null, null, "21 min per ep", 2, 160, "https://cdn.myanimelist.net/images/anime/11/35863.jpg", "https://myanimelist.net/anime/8063/Sekaiichi_Hatsukoi_OVA", 60431, 2079, null, 618, "PG-13 - Teens 13 or older", 58073, true, "request:anime:a940504067fab89ee0a22ed38a16aaa94461a45d", 7.94f, 31946, "Manga", "Finished Airing", "Two OVA episodes featuring additional stories. Episode 0: Ritsu and Takano used to date back in high school, but broke up due to a misunderstanding. This is that story. Episode 12.5: Hatori and Chiaki go to visit the latter's family. It's really awkward in many, many ways.", "Sekaiichi Hatsukoi OVA", null, "世界一初恋 OVA", "OVA" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "MALSubItem");

            migrationBuilder.DropTable(
                name: "ScheduledAnimes");
        }
    }
}
