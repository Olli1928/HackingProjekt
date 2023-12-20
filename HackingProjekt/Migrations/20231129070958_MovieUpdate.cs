using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackingProjekt.Migrations
{
    public partial class MovieUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Movie",
                newName: "Udkomst");

            migrationBuilder.AddColumn<string>(
                name: "Andmenelse",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Andmenelse",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "Udkomst",
                table: "Movie",
                newName: "ReleaseDate");
        }
    }
}
