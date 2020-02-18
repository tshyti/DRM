using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DRM_Api.Persistance.Migrations
{
    public partial class SetMimeTypeSizeto100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "UserFiles",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.UpdateData(
                table: "UserFiles",
                keyColumn: "Id",
                keyValue: new Guid("88d18a79-9bf8-4ad2-b2d7-c062ee6987e3"),
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "UserFiles",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "UserFiles",
                keyColumn: "Id",
                keyValue: new Guid("88d18a79-9bf8-4ad2-b2d7-c062ee6987e3"),
                column: "IsActive",
                value: true);
        }
    }
}
