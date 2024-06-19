using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PageSage.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLinks",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "UserBooks");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginDate",
                table: "UserBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFinished",
                table: "UserBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginDate",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "HasFinished",
                table: "UserBooks");

            migrationBuilder.AddColumn<string>(
                name: "ImageLinks",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishedDate",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
