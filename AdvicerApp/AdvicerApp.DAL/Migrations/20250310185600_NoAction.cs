using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvicerApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Comments_CommentId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Reports",
                newName: "CommmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CommentId",
                table: "Reports",
                newName: "IX_Reports_CommmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Comments_CommmentId",
                table: "Reports",
                column: "CommmentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Comments_CommmentId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "CommmentId",
                table: "Reports",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CommmentId",
                table: "Reports",
                newName: "IX_Reports_CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Comments_CommentId",
                table: "Reports",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
