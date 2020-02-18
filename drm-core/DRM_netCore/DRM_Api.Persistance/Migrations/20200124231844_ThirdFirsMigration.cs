using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DRM_Api.Persistance.Migrations
{
    public partial class ThirdFirsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Token = table.Column<string>(maxLength: 50, nullable: true),
                    Expiration = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Surname = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RefreshTokenId = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    ModifiedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_RefreshTokens_RefreshTokenId",
                        column: x => x.RefreshTokenId,
                        principalTable: "RefreshTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    ModifiedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Roles_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: false),
                    AzureName = table.Column<string>(maxLength: 250, nullable: false),
                    MimeType = table.Column<string>(maxLength: 25, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    OwnerId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    ModifiedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFiles_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFiles_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFiles_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    ModifiedById = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Email", "IsActive", "ModifiedById", "ModifiedOn", "Name", "Password", "RefreshTokenId", "Surname", "Username" },
                values: new object[] { new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "some@email.com", true, new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name", "AID15NptuAnOuKL4W4TQ/FU0zNNst5ouANLHUCP1NbF+6l52j3Dbfk40LB6leJJzgg==", null, "Surname", "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "IsActive", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { new Guid("7b68c198-2192-45e3-b908-6bb4c5af159f"), new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "IsActive", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { new Guid("764d55f7-beab-40be-8d62-076e5d25f01a"), new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Common" });

            migrationBuilder.InsertData(
                table: "UserFiles",
                columns: new[] { "Id", "AzureName", "CreatedById", "CreatedOn", "Description", "IsActive", "MimeType", "ModifiedById", "ModifiedOn", "Name", "OwnerId", "URL" },
                values: new object[] { new Guid("88d18a79-9bf8-4ad2-b2d7-c062ee6987e3"), "Volcano 4K.png", new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a volcano render.", true, "image/png", new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Volcano", new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), "https://drmprojectaccount.blob.core.windows.net/drmblob-container/Volcano 4K.png" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "ModifiedById", "ModifiedOn", "RoleId", "UserId" },
                values: new object[] { new Guid("a662f2ab-bb52-423a-8cb1-04be993887e3"), new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b68c198-2192-45e3-b908-6bb4c5af159f"), new Guid("8bdc99e6-8b9b-46a0-815c-a4a138996215") });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedById",
                table: "Roles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ModifiedById",
                table: "Roles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_ModifiedById",
                table: "User",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_RefreshTokenId",
                table: "User",
                column: "RefreshTokenId",
                unique: true,
                filter: "[RefreshTokenId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_CreatedById",
                table: "UserFiles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_ModifiedById",
                table: "UserFiles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_OwnerId",
                table: "UserFiles",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_CreatedById",
                table: "UserRoles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ModifiedById",
                table: "UserRoles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFiles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RefreshTokens");
        }
    }
}
