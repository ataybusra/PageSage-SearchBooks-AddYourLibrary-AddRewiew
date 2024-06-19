using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PageSage.Migrations
{
    public partial class roleidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleBookItemId",
                table: "UserBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleBookItemId",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
