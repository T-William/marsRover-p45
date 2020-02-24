using Microsoft.EntityFrameworkCore.Migrations;

namespace MarsRover.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grid",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfRovers = table.Column<int>(nullable: false),
                    GridSizeX = table.Column<int>(nullable: false),
                    GridSizeY = table.Column<int>(nullable: false),
                    GridTotalSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rover",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TotalMovements = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rover", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoverMovement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoverId = table.Column<int>(nullable: false),
                    BeginX = table.Column<int>(nullable: false),
                    BeginY = table.Column<int>(nullable: false),
                    BeginOrientation = table.Column<string>(nullable: true),
                    MovementInput = table.Column<string>(nullable: true),
                    EndX = table.Column<int>(nullable: true),
                    EndY = table.Column<int>(nullable: true),
                    EndOrientation = table.Column<string>(nullable: true)
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
                column: "RoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grid");

            migrationBuilder.DropTable(
                name: "RoverMovement");

            migrationBuilder.DropTable(
                name: "Rover");
        }
    }
}
