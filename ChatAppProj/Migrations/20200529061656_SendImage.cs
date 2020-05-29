using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class SendImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupProfiles_AspNetUsers_ProfileId",
                table: "GroupProfiles");

            migrationBuilder.DropTable(
                name: "ChannelProfiles");

            migrationBuilder.DropIndex(
                name: "IX_GroupProfiles_ProfileId",
                table: "GroupProfiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GroupProfiles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupProfiles");

            migrationBuilder.CreateTable(
                name: "ChannelProfiles",
                columns: table => new
                {
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_GroupProfiles_ProfileId",
                table: "GroupProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelProfiles_GroupId",
                table: "ChannelProfiles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelProfiles_ProfileId",
                table: "ChannelProfiles",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupProfiles_AspNetUsers_ProfileId",
                table: "GroupProfiles",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
