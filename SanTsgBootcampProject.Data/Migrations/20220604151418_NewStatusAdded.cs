using Microsoft.EntityFrameworkCore.Migrations;

namespace SanTsgBootcampProject.Data.Migrations
{
    public partial class NewStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
