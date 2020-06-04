using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class DomainRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chats_ChannelId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "ProfileName",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "ProfileName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "GroupProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "TimeRegistrations",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "TimeRegistrations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TimeRegistrations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "TimeRegistrations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Messages",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Privacy",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "GroupProfiles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "GroupProfiles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ChannelId",
                table: "Chats",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Channels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Channels",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ChatId",
                table: "Channels",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrations_ProfileId",
                table: "TimeRegistrations",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProfileId",
                table: "Messages",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupProfiles_ProfileId",
                table: "GroupProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChannelId",
                table: "Chats",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_ChatId",
                table: "Channels",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Chats_ChatId",
                table: "Channels",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupProfiles_AspNetUsers_ProfileId",
                table: "GroupProfiles",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ProfileId",
                table: "Messages",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_AspNetUsers_ProfileId",
                table: "TimeRegistrations",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Chats_ChatId",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupProfiles_AspNetUsers_ProfileId",
                table: "GroupProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ProfileId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_AspNetUsers_ProfileId",
                table: "TimeRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TimeRegistrations_ProfileId",
                table: "TimeRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ProfileId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_GroupProfiles_ProfileId",
                table: "GroupProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ChannelId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Channels_ChatId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Privacy",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Channels");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "TimeRegistrations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "TimeRegistrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "TimeRegistrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfileName",
                table: "TimeRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ProfileName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "GroupProfiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "GroupProfiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "GroupProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Chats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Channels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Channels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChannelId",
                table: "Chats",
                column: "ChannelId",
                unique: true);
        }
    }
}
