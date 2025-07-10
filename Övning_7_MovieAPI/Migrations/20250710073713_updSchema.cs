using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Övning_7_MovieAPI.Migrations
{
    /// <inheritdoc />
    public partial class updSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Movies_MovieId",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "MovieDetails");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_MovieId",
                table: "MovieDetails",
                newName: "IX_MovieDetails_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieDetails",
                table: "MovieDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDetails_Movies_MovieId",
                table: "MovieDetails",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDetails_Movies_MovieId",
                table: "MovieDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieDetails",
                table: "MovieDetails");

            migrationBuilder.RenameTable(
                name: "MovieDetails",
                newName: "Movie");

            migrationBuilder.RenameIndex(
                name: "IX_MovieDetails_MovieId",
                table: "Movie",
                newName: "IX_Movie_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Movies_MovieId",
                table: "Movie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
