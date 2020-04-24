using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class ChannelProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatProfiles");

            migrationBuilder.CreateTable(
                name: "ChannelProfiles",
                columns: table => new
                {
                    ProfileId = table.Column<string>(nullable: false),
                    ChannelId = table.Column<int>(nullable: false),
                    ChatId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelProfiles", x => new { x.ChannelId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ChannelProfiles_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChannelProfiles_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChannelProfiles_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChannelProfiles_AspNetUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelProfiles_ChatId",
                table: "ChannelProfiles",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelProfiles_GroupId",
                table: "ChannelProfiles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelProfiles_ProfileId",
                table: "ChannelProfiles",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelProfiles");

            migrationBuilder.CreateTable(
                name: "ChatProfiles",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatProfiles", x => new { x.ChatId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ChatProfiles_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatProfiles_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatProfiles_AspNetUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatProfiles_GroupId",
                table: "ChatProfiles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatProfiles_ProfileId",
                table: "ChatProfiles",
                column: "ProfileId");
        }
    }
}
