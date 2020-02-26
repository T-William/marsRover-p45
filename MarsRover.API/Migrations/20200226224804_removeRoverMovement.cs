using Microsoft.EntityFrameworkCore.Migrations;

namespace MarsRover.API.Migrations
{
    public partial class removeRoverMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoverMovement");

            migrationBuilder.AddColumn<string>(
                name: "BeginOrientation",
                table: "Rover",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BeginX",
                table: "Rover",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BeginY",
                table: "Rover",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EndOrientation",
                table: "Rover",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndX",
                table: "Rover",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndY",
                table: "Rover",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MovementInput",
                table: "Rover",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginOrientation",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "BeginX",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "BeginY",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "EndOrientation",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "EndX",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "EndY",
                table: "Rover");

            migrationBuilder.DropColumn(
                name: "MovementInput",
                table: "Rover");

            migrationBuilder.CreateTable(
                name: "RoverMovement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeginOrientation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginX = table.Column<int>(type: "int", nullable: false),
                    BeginY = table.Column<int>(type: "int", nullable: false),
                    EndOrientation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndX = table.Column<int>(type: "int", nullable: true),
                    EndY = table.Column<int>(type: "int", nullable: true),
                    MovementInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoverMovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoverMovement_Rover_RoverId",
                        column: x => x.RoverId,
                        principalTable: "Rover",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoverMovement_RoverId",
                table: "RoverMovement",
                column: "RoverId",
                unique: true);
        }
    }
}
