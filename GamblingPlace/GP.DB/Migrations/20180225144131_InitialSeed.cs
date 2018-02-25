using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GP.DB.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomExceptions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CustomInnerMessage = table.Column<string>(nullable: true),
                    CustomInnerStackTrace = table.Column<string>(nullable: true),
                    CustomMessage = table.Column<string>(nullable: true),
                    CustomStackTrace = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    HResult = table.Column<int>(nullable: false),
                    HelpLink = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomExceptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Coins = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    ValidationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomExceptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
