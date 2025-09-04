using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITR.API.BLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadyToLecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Video",
                table: "Lectures");

            migrationBuilder.AddColumn<bool>(
                name: "IsReady",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReady",
                table: "Lectures");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
