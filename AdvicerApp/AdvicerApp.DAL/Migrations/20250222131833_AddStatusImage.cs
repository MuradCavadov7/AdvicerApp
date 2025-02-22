using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvicerApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Statuses",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Statuses");
        }
    }
}
