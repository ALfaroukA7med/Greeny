using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greeny.DAL.Migrations
{
    /// <inheritdoc />
    public partial class votepost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId_PostId",
                table: "Votes",
                columns: new[] { "UserId", "PostId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_UserId_PostId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
