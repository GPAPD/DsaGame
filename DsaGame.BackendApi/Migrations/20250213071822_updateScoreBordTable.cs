using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsaGame.BackendApi.Migrations
{
    /// <inheritdoc />
    public partial class updateScoreBordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Level",
                table: "ScoreBoards",
                newName: "GameLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameLevel",
                table: "ScoreBoards",
                newName: "Level");
        }
    }
}
