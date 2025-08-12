using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pentagramm.Migrations
{
    /// <inheritdoc />
    public partial class BeforeTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMember_AspNetUsers_UserId",
                table: "ChatMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMember_Chats_ChatId",
                table: "ChatMember");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMember",
                table: "ChatMember");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatMember");

            migrationBuilder.RenameTable(
                name: "ChatMember",
                newName: "ChatMembers");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMember_ChatId",
                table: "ChatMembers",
                newName: "IX_ChatMembers_ChatId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Chats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedAt",
                table: "ChatMembers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ChatMembers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMembers",
                table: "ChatMembers",
                columns: new[] { "UserId", "ChatId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMembers_AspNetUsers_UserId",
                table: "ChatMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMembers_Chats_ChatId",
                table: "ChatMembers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMembers_AspNetUsers_UserId",
                table: "ChatMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMembers_Chats_ChatId",
                table: "ChatMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMembers",
                table: "ChatMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "JoinedAt",
                table: "ChatMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ChatMembers");

            migrationBuilder.RenameTable(
                name: "ChatMembers",
                newName: "ChatMember");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMembers_ChatId",
                table: "ChatMember",
                newName: "IX_ChatMember_ChatId");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ChatMember",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMember",
                table: "ChatMember",
                columns: new[] { "UserId", "ChatId" });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsReaded = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMember_AspNetUsers_UserId",
                table: "ChatMember",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMember_Chats_ChatId",
                table: "ChatMember",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
