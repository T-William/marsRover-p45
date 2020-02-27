using Microsoft.EntityFrameworkCore.Migrations;

namespace MarsRover.API.Migrations
{
    public partial class removerNumberORovers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRovers",
                table: "Grid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRovers",
                table: "Grid",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
