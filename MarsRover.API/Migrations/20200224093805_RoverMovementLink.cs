using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarsRover.API.Migrations
{
    public partial class RoverMovementLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MovementDate",
                table: "RoverMovement",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalDeployments",
                table: "Rover",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovementDate",
                table: "RoverMovement");

            migrationBuilder.DropColumn(
                name: "TotalDeployments",
                table: "Rover");
        }
    }
}
