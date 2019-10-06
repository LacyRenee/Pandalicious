using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandalicious.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuMeasurement",
                table: "Menus",
                newName: "MenuValue");

            migrationBuilder.AddColumn<string>(
                name: "MenuUnit",
                table: "Menus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuUnit",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "MenuValue",
                table: "Menus",
                newName: "MenuMeasurement");
        }
    }
}
