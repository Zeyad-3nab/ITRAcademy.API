using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITR.API.BLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadyToLectureName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Namme",
                table: "Lectures",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lectures",
                newName: "Namme");
        }
    }
}
