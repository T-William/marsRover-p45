using Microsoft.EntityFrameworkCore.Migrations;

namespace MarsRover.API.Migrations
{
    public partial class RoverGridLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rover",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GridId",
                table: "Rover",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarsGridId",
                table: "Rover",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GridName",
                table: "Grid",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rover_MarsGridId",
                table: "Rover",
                column: "MarsGridId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rover_Grid_MarsGridId",
                table: "Rover",
                column: "MarsGridId",
                principalTable: "Grid",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rover_Grid_MarsGridId",
                table: "Rover");

            migrationBuilder.DropIndex(
                name: "IX_Rover_MarsGridId",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "GridId",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "MarsGridId",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "GridName",
                table: "Grid");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rover",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);
        }
    }
}
