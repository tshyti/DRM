using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DRM_Api.Persistance.Migrations
{
    public partial class AddedUserStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownloadNo",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UploadNo",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "UserFiles",
                keyColumn: "Id",
                keyValue: new Guid("88d18a79-9bf8-4ad2-b2d7-c062ee6987e3"),
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadNo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UploadNo",
                table: "User");

            migrationBuilder.UpdateData(
                table: "UserFiles",
                keyColumn: "Id",
                keyValue: new Guid("88d18a79-9bf8-4ad2-b2d7-c062ee6987e3"),
                column: "IsActive",
                value: true);
        }
    }
}
