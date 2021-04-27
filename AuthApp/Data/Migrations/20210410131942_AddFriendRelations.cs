using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApp.Data.Migrations
{
    public partial class AddFriendRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRelation_AspNetUsers_FriendId",
                table: "FriendRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRelation_AspNetUsers_UserId",
                table: "FriendRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRelation",
                table: "FriendRelation");

            migrationBuilder.RenameTable(
                name: "FriendRelation",
                newName: "FriendRelations");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRelation_FriendId",
                table: "FriendRelations",
                newName: "IX_FriendRelations_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRelations",
                table: "FriendRelations",
                columns: new[] { "UserId", "FriendId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRelations_AspNetUsers_FriendId",
                table: "FriendRelations",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRelations_AspNetUsers_UserId",
                table: "FriendRelations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRelations_AspNetUsers_FriendId",
                table: "FriendRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRelations_AspNetUsers_UserId",
                table: "FriendRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRelations",
                table: "FriendRelations");

            migrationBuilder.RenameTable(
                name: "FriendRelations",
                newName: "FriendRelation");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRelations_FriendId",
                table: "FriendRelation",
                newName: "IX_FriendRelation_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRelation",
                table: "FriendRelation",
                columns: new[] { "UserId", "FriendId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRelation_AspNetUsers_FriendId",
                table: "FriendRelation",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRelation_AspNetUsers_UserId",
                table: "FriendRelation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
