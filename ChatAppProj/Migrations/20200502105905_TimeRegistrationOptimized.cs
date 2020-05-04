using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class TimeRegistrationOptimized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_AspNetUsers_ProfileId",
                table: "TimeRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TimeRegistrations_ProfileId",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "TimeEntered",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "TimeLeft",
                table: "TimeRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "TimeRegistrations",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "TimeRegistrations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfileName",
                table: "TimeRegistrations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "ProfileName",
                table: "TimeRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "TimeRegistrations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeEntered",
                table: "TimeRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeLeft",
                table: "TimeRegistrations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrations_ProfileId",
                table: "TimeRegistrations",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_AspNetUsers_ProfileId",
                table: "TimeRegistrations",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
