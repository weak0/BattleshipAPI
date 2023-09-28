using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleShipAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoard_Players_PlayerId",
                table: "GameBoard");

            migrationBuilder.DropForeignKey(
                name: "FK_Ship_GameBoard_BoardId",
                table: "Ship");

            migrationBuilder.DropTable(
                name: "Cordinates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ship",
                table: "Ship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameBoard",
                table: "GameBoard");

            migrationBuilder.RenameTable(
                name: "Ship",
                newName: "Ships");

            migrationBuilder.RenameTable(
                name: "GameBoard",
                newName: "GameBoards");

            migrationBuilder.RenameIndex(
                name: "IX_Ship_BoardId",
                table: "Ships",
                newName: "IX_Ships_BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_GameBoard_PlayerId",
                table: "GameBoards",
                newName: "IX_GameBoards_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ships",
                table: "Ships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameBoards",
                table: "GameBoards",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cordiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cord = table.Column<int>(type: "int", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cordiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cordiantes_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cordiantes_ShipId",
                table: "Cordiantes",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_PlayerId",
                table: "GameBoards",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_GameBoards_BoardId",
                table: "Ships",
                column: "BoardId",
                principalTable: "GameBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_PlayerId",
                table: "GameBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_GameBoards_BoardId",
                table: "Ships");

            migrationBuilder.DropTable(
                name: "Cordiantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ships",
                table: "Ships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameBoards",
                table: "GameBoards");

            migrationBuilder.RenameTable(
                name: "Ships",
                newName: "Ship");

            migrationBuilder.RenameTable(
                name: "GameBoards",
                newName: "GameBoard");

            migrationBuilder.RenameIndex(
                name: "IX_Ships_BoardId",
                table: "Ship",
                newName: "IX_Ship_BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_GameBoards_PlayerId",
                table: "GameBoard",
                newName: "IX_GameBoard_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ship",
                table: "Ship",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameBoard",
                table: "GameBoard",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cordinates",
                columns: table => new
                {
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cordinates", x => new { x.ShipId, x.Id });
                    table.ForeignKey(
                        name: "FK_Cordinates_Ship_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoard_Players_PlayerId",
                table: "GameBoard",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ship_GameBoard_BoardId",
                table: "Ship",
                column: "BoardId",
                principalTable: "GameBoard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
