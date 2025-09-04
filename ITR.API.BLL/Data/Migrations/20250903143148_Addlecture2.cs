using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITR.API.BLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addlecture2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualities",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Qualities",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Lectures");
        }
    }
}
