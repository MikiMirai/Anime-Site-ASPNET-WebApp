using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_WebApp.Infrastructure.Migrations
{
    public partial class GenresRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Genre_GenresId",
                schema: "Identity",
                table: "AnimeGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                schema: "Identity",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Genre",
                schema: "Identity",
                newName: "Genres",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                schema: "Identity",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Genres_GenresId",
                schema: "Identity",
                table: "AnimeGenre",
                column: "GenresId",
                principalSchema: "Identity",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Genres_GenresId",
                schema: "Identity",
                table: "AnimeGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                schema: "Identity",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                schema: "Identity",
                newName: "Genre",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                schema: "Identity",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Genre_GenresId",
                schema: "Identity",
                table: "AnimeGenre",
                column: "GenresId",
                principalSchema: "Identity",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
