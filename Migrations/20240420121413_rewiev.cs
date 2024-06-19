using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PageSage.Migrations
{
    public partial class rewiev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_AppUserId",
                table: "UserBooks");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserBooks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_AppUserId",
                table: "UserBooks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_AppUserId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "UserBooks");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserBooks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_AppUserId",
                table: "UserBooks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
