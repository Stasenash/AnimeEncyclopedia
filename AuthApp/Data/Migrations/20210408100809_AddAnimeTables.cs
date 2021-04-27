using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApp.Data.Migrations
{
    public partial class AddAnimeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AnimeLabels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendRelation",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    FriendId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRelation", x => new { x.UserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_FriendRelation_AspNetUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FriendRelation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimeLabels",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AnimeId = table.Column<long>(nullable: false),
                    LabelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimeLabels", x => new { x.UserId, x.AnimeId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_UserAnimeLabels_AnimeLabels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "AnimeLabels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimeLabels_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnimeLabels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Liked" },
                    { 2, "Disliked" },
                    { 3, "Watched" },
                    { 4, "Dropped" },
                    { 5, "Won't watch" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 0, "271801b3-57ba-44f4-a84b-c8d149e691bc", "AppUser", "test@test.com", true, true, null, "TEST@TEST.COM", "TEST@TEST.COM", "AQAAAAEAACcQAAAAEL6eBpwetmHd6NziWJg86HtmxOyznY2CUyRaYAlGxEdU423LR18jK5rXYu8pCTWpGw==", null, false, "E4IMYKVLO574KFHY5KLX4YHR46TFGK4O", false, "test@test.com" });

            migrationBuilder.InsertData(
                table: "UserAnimeLabels",
                columns: new[] { "UserId", "AnimeId", "LabelId" },
                values: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 11123L, 1 });

            migrationBuilder.InsertData(
                table: "UserAnimeLabels",
                columns: new[] { "UserId", "AnimeId", "LabelId" },
                values: new object[] { "f2bc0937-52d6-4012-bc65-91535266799d", 11123L, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRelation_FriendId",
                table: "FriendRelation",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeLabels_LabelId",
                table: "UserAnimeLabels",
                column: "LabelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRelation");

            migrationBuilder.DropTable(
                name: "UserAnimeLabels");

            migrationBuilder.DropTable(
                name: "AnimeLabels");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2bc0937-52d6-4012-bc65-91535266799d");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
