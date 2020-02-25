using Microsoft.EntityFrameworkCore.Migrations;

namespace MarsRover.API.Migrations
{
    public partial class roverchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rover_Grid_MarsGridId",
                table: "Rover");

            migrationBuilder.DropIndex(
                name: "IX_Rover_MarsGridId",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "MarsGridId",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "TotalDeployments",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "TotalMovements",
                table: "Rover");

            migrationBuilder.AlterColumn<int>(
                name: "GridId",
                table: "Rover",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rover_GridId",
                table: "Rover",
                column: "GridId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rover_Grid_GridId",
                table: "Rover",
                column: "GridId",
                principalTable: "Grid",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rover_Grid_GridId",
                table: "Rover");

            migrationBuilder.DropIndex(
                name: "IX_Rover_GridId",
                table: "Rover");

            migrationBuilder.AlterColumn<int>(
                name: "GridId",
                table: "Rover",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MarsGridId",
                table: "Rover",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalDeployments",
                table: "Rover",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalMovements",
                table: "Rover",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
