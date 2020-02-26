using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarsRover.API.Migrations
{
    public partial class rovermovementchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoverMovement_RoverId",
                table: "RoverMovement");

            migrationBuilder.DropColumn(
                name: "MovementDate",
                table: "RoverMovement");

            migrationBuilder.CreateIndex(
                name: "IX_RoverMovement_RoverId",
                table: "RoverMovement",
                column: "RoverId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoverMovement_RoverId",
                table: "RoverMovement");

            migrationBuilder.AddColumn<DateTime>(
                name: "MovementDate",
                table: "RoverMovement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_RoverMovement_RoverId",
                table: "RoverMovement",
                column: "RoverId");
        }
    }
}
