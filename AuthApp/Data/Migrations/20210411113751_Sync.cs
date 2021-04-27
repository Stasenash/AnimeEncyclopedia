using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApp.Data.Migrations
{
    public partial class Sync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserAnimeLabels",
                keyColumns: new[] { "UserId", "AnimeId", "LabelId" },
                keyValues: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 11123L, 1 });

            migrationBuilder.DeleteData(
                table: "UserAnimeLabels",
                keyColumns: new[] { "UserId", "AnimeId", "LabelId" },
                keyValues: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 11123L, 3 });

            migrationBuilder.InsertData(
                table: "UserAnimeLabels",
                columns: new[] { "UserId", "AnimeId", "LabelId" },
                values: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 8063L, 1 });

            migrationBuilder.InsertData(
                table: "UserAnimeLabels",
                columns: new[] { "UserId", "AnimeId", "LabelId" },
                values: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 8063L, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserAnimeLabels",
                keyColumns: new[] { "UserId", "AnimeId", "LabelId" },
                keyValues: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 8063L, 1 });

            migrationBuilder.DeleteData(
                table: "UserAnimeLabels",
                keyColumns: new[] { "UserId", "AnimeId", "LabelId" },
                keyValues: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 8063L, 3 });

            migrationBuilder.InsertData(
                table: "UserAnimeLabels",
                columns: new[] { "UserId", "AnimeId", "LabelId" },
                values: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 11123L, 1 });

            migrationBuilder.InsertData(
                table: "UserAnimeLabels",
                columns: new[] { "UserId", "AnimeId", "LabelId" },
                values: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 11123L, 3 });
        }
    }
}
